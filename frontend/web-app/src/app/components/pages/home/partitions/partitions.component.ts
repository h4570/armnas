import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { assert } from 'src/app/assert';
import { smoothHeight } from 'src/app/components/shared/animations';
import { LsblkPartitionInfoView } from 'src/app/components/pages/home/partitions/view-models/lsblk-partition-info.view-model';
import { Partition } from 'src/app/models/odata/partition.model';
import { AppService } from 'src/app/services/app.service';
import { ODataService } from 'src/app/services/odata.service';
import { SystemInformationService } from 'src/app/services/system-information.service';
import { REFRESH_OFF_VALUE } from '../home.component';
import { getLsblkDiskInfoViewModels, LsblkDiskInfoView } from './view-models/lsblk-disk-info.view-model';
import { PartitionService } from 'src/app/services/partition.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogButtonType, DialogType } from 'src/app/components/shared/fast-dialog/fast-dialog.component';

@Component({
  selector: 'app-partitions',
  templateUrl: './partitions.component.html',
  styleUrls: ['./partitions.component.scss'],
  animations: [smoothHeight]
})
export class PartitionsComponent implements OnInit, OnDestroy {

  @Input() public refreshInterval: number;

  public disks: LsblkDiskInfoView[];
  public loading = true;
  public partitionsRefresh = false;
  private isNgDestroyed = false;

  constructor(
    public readonly appService: AppService,
    public readonly translate: TranslateService,
    private readonly snackbar: MatSnackBar,
    private readonly sysInfoService: SystemInformationService,
    private readonly partitionService: PartitionService,
    private readonly fastDialog: FastDialogService,
    private readonly odata: ODataService
  ) { }

  public async ngOnInit(): Promise<void> {
    try {
      await this.load();
    } catch {
      this.loading = false;
      // do nothing
    }
    setTimeout(async () => await this.refresh(), this.refreshInterval);
  }

  public async refresh(onDemand: boolean = false): Promise<void> {
    try {
      if (!this.isNgDestroyed && this.refreshInterval !== REFRESH_OFF_VALUE)
        await this.load();
      if (!onDemand)
        setTimeout(() => this.refresh(), this.refreshInterval);
    } catch {
      this.loading = false;
      // do nothing
    }
  }

  public ngOnDestroy(): void {
    this.isNgDestroyed = true;
  }

  public async onEditClick(partition: LsblkPartitionInfoView): Promise<void> {
    partition.isInEditMode = true;
  }

  public async onSaveClick(partition: LsblkPartitionInfoView): Promise<void> {
    await this.updateDisplayNameInDb(partition);
    partition.updateCachedDisplayName();
    partition.isInEditMode = false;
    this.snackbar.open('Done!', 'OK', { duration: 2000 });
  }

  public async onCancelEditClick(partition: LsblkPartitionInfoView): Promise<void> {
    partition.displayName = partition.cachedDisplayName;
    partition.isInEditMode = false;
  }

  public async onMountClick(partition: LsblkPartitionInfoView): Promise<void> {
    if (!partition.displayName) {
      this.snackbar.open('Please set display name first!', 'Got it!', { duration: 3000 });
      this.appService.isInTweakMode = true;
      partition.isInEditMode = true;
      return;
    }
    try {
      await this.partitionService.mount(partition.uuid);
      await this.refresh(true);
      this.snackbar.open('Done!', 'Ok!', { duration: 3000 });
    } catch (raw) {
      const err = raw as HttpErrorResponse;
      const title = this.translate.instant('common.error') as string;
      const text = [this.translate.instant(err.error) as string];
      await this.fastDialog.open(DialogType.error, DialogButtonType.ok, title, text);
    }
  }

  public async onUnmountClick(partition: LsblkPartitionInfoView): Promise<void> {
    try {
      await this.partitionService.unmount(partition.uuid);
      await this.refresh(true);
      this.snackbar.open('Done!', 'Ok!', { duration: 3000 });
    } catch (raw) {
      const err = raw as HttpErrorResponse;
      const title = this.translate.instant('common.error') as string;
      const text = [this.translate.instant(err.error) as string];
      await this.fastDialog.open(DialogType.error, DialogButtonType.ok, title, text);
    }
  }

  private async load(): Promise<void> {
    const newDisks = getLsblkDiskInfoViewModels(await this.sysInfoService.getDisksInfo());
    if (!this.disks) {
      this.disks = newDisks;
      await this.fillAllPartitionDisplayNamesFromDb();
    }
    else if (this.disks && newDisks.length !== this.disks.length) {
      this.partitionsRefresh = !this.partitionsRefresh;
      this.fillAllPartitionDisplayNamesFromOldData(newDisks);
      this.disks = newDisks;
      await this.fillAllPartitionDisplayNamesFromDb();
    }
    this.loading = false;
  }

  private get partitions(): LsblkPartitionInfoView[] {
    return this.disks.reduce((acc, c) => acc.concat(c.partitions), [] as LsblkPartitionInfoView[]); // flat map
  }

  /** Fill display name from already downloaded disks to new disks partitions that dont have partition name. */
  private fillAllPartitionDisplayNamesFromOldData(newDisks: LsblkDiskInfoView[]): void {
    assert(this.disks && this.disks.length > 0);
    newDisks.forEach(newDisk => newDisk.partitions.forEach(partition => {
      if (!partition.displayName) {
        const cache = this.partitions.find(c => c.uuid === partition.uuid);
        if (cache)
          partition.displayName = cache.displayName;
      }
    }));
  }

  /** Fill display name from database to partitions that dont have partition name. */
  private async fillAllPartitionDisplayNamesFromDb(): Promise<void> {
    assert(this.disks && this.disks.length > 0);
    const payload = this.partitions.map(c => c.uuid);
    const dbParts = await this.getDisplayNamesFromDb(payload);
    this.disks.forEach(disk => disk.partitions.forEach(partition => {
      if (!partition.displayName) {
        const dbPart = dbParts.find(c => c.uuid === partition.uuid);
        if (dbPart) {
          partition.displayName = dbPart.displayName;
          partition.dbId = dbPart.id;
          partition.updateCachedDisplayName();
        }
      }
    }));
  }

  private async getDisplayNamesFromDb(uuids: string[]): Promise<Partition[]> {
    const partitions = this.odata.partitions.entities();
    const filterArg: { uuid: string }[] = [];
    uuids.forEach(uuid => filterArg.push({ uuid }));
    const res = partitions
      .filter({ or: filterArg })
      .get()
      .toPromise()
      .then(c => c.entities);
    return await res;
  }

  /** If display name exist in db -> update it, otherwise -> add */
  private async updateDisplayNameInDb(partition: LsblkPartitionInfoView): Promise<void> {
    const payload: Partition = { id: 0, uuid: partition.uuid, displayName: partition.displayName };
    const partitions = this.odata.partitions.entities();
    if (partition.dbId) {
      const ref = partitions.entity(partition.dbId);
      await ref.patch(payload)
        .toPromise();
    } else {
      const res = await partitions
        .post(payload)
        .toPromise();
      partition.dbId = res.entity.id;
    }
  }

}

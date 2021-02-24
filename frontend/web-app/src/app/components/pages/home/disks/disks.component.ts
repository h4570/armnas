import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { smoothHeight } from 'src/app/components/shared/animations';
import { getLsblkDiskInfoViewModels, LsblkDiskInfoView } from 'src/app/components/view-models/os-commander/lsblk-disk-info.view-model';
import { LsblkPartitionInfoView } from 'src/app/components/view-models/os-commander/partition-info/lsblk-partition-info.view-model';
import { LsblkPartitionInfo } from 'src/app/models/os-commander/partition-info/lsblk-partition-info.model';
import { AppService } from 'src/app/services/app.service';
import { SystemInformationService } from 'src/app/services/system-information.service';
import { REFRESH_OFF_VALUE } from '../home.component';

@Component({
  selector: 'app-disks',
  templateUrl: './disks.component.html',
  styleUrls: ['./disks.component.scss'],
  animations: [smoothHeight]
})
export class DisksComponent implements OnInit, OnDestroy {

  @Input() public refreshInterval: number;

  public disks: LsblkDiskInfoView[];
  public loading = true;
  public disksRefresh = false;
  private isNgDestroyed = false;

  constructor(
    public readonly appService: AppService,
    private readonly sysInfoService: SystemInformationService,
    public readonly translate: TranslateService
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.load();
    setTimeout(async () => await this.refresh(), this.refreshInterval);
  }

  public async refresh(): Promise<void> {
    if (this.isNgDestroyed || this.refreshInterval === REFRESH_OFF_VALUE)
      return;
    await this.load();
    setTimeout(() => this.refresh(), this.refreshInterval);
  }

  public ngOnDestroy(): void {
    this.isNgDestroyed = true;
  }

  public async onEditClick(partition: LsblkPartitionInfoView): Promise<void> {
    partition.isInEditMode = true;
  }

  public async onSaveClick(partition: LsblkPartitionInfoView): Promise<void> {
    // save displayName
    partition.isInEditMode = false;
  }

  public async onCancelEditClick(partition: LsblkPartitionInfoView): Promise<void> {
    partition.displayName = undefined;
    partition.isInEditMode = false;
  }

  public async onMountClick(partition: LsblkPartitionInfoView): Promise<void> {

  }

  public async onUnmountClick(partition: LsblkPartitionInfoView): Promise<void> {

  }

  private async load(): Promise<void> {
    const disks = getLsblkDiskInfoViewModels(await this.sysInfoService.getDisksInfo());
    if (!this.disks) {
      this.disks = disks;
      // get all displayName's from API
    }
    else if (this.disks && disks.length !== this.disks.length) {
      this.disksRefresh = !this.disksRefresh;
      // loop
      // --if device exist here -> fill displayName from here
      // --if not -> fill displayName from API
      this.disks = disks;
    }
    this.loading = false;
  }

}

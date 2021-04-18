import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { CronService } from 'src/app/services/cron.service';
import { ODataService } from 'src/app/services/odata.service';
import { smoothHeight } from '../../shared/animations';
import { CronEntryViewModel } from './view-models/cron-entry.view-model';

@Component({
  selector: 'app-cron',
  templateUrl: './cron.component.html',
  styleUrls: ['./cron.component.scss'],
  animations: [smoothHeight]
})
export class CronComponent implements OnInit {

  public isSaving: boolean;
  public isLoading = true;

  public entries: CronEntryViewModel[] = [];
  public viewEntries: CronEntryViewModel[] = [];

  constructor(
    public readonly appService: AppService,
    public readonly cronService: CronService,
    public readonly odata: ODataService,
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.refresh();
  }

  public async onAddClick(): Promise<void> {
    const newObj = new CronEntryViewModel({
      cron: '',
      command: '',
    }, false);
    this.entries.unshift(newObj);
    this.viewEntries.unshift(newObj);
  }

  public async onDeleteClick(entry: CronEntryViewModel): Promise<void> {
    if (entry.isInCronAlready) {
      entry.markAsDeleted();
    } else {
      this.entries = this.entries.filter(c => c.id !== entry.id);
    }
    this.viewEntries = this.viewEntries.filter(c => c.id !== entry.id);
  }

  public async onSaveClick(): Promise<void> {
    this.isSaving = true;
    try {
      for (const entry of this.entries) {
        if (entry.isDeleted) {
          // Delete
          await this.cronService.delete(entry.originalModel);
        } else if (entry.isInCronAlready && entry.wasModified) {
          // (Update) -> Delete, Create
          await this.cronService.delete(entry.originalModel);
          await this.cronService.create(entry.model);
        } else if (!entry.isInCronAlready) {
          // Create
          await this.cronService.create(entry.model);
        }
      }
    } catch {
      // this is intentional, err is handled in cronService
    }
    await this.refresh();
    this.isSaving = false;
  }

  private async refresh(): Promise<void> {
    this.isLoading = true;
    const odataPartitions = this.odata.partitions.entities();
    const partitions = await odataPartitions
      .get()
      .toPromise()
      .then(c => c.entities);
    const tempEntries: CronEntryViewModel[] = [];
    const crontab = await this.cronService.getAll();
    for (const entry of crontab) {
      const obj = new CronEntryViewModel(entry, true);
      obj.updateIsArmansMountingPoint(partitions);
      tempEntries.push(obj);
    }
    this.clearAndInsertSortedEntries(tempEntries);
    this.isLoading = false;
  }

  private clearAndInsertSortedEntries(drawEntries: CronEntryViewModel[]) {
    const sorted = drawEntries.sort(
      (x, y) =>
        (x.isArmansMountingPoint || x.isArmansStartScript)
          &&
          (!y.isArmansMountingPoint && !y.isArmansStartScript)
          ? 1 : -1);
    this.entries = [];
    this.viewEntries = [];
    sorted.forEach(entry => { this.entries.push(entry); this.viewEntries.push(entry); });
  }

}

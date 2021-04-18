import { Component, OnInit } from '@angular/core';
import { Partition } from 'src/app/models/partition.model';
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

  constructor(
    public readonly appService: AppService,
    public readonly cronService: CronService,
    public readonly odata: ODataService,
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.refresh();
  }

  public async onAddClick(): Promise<void> {
    this.entries.unshift(new CronEntryViewModel({
      cron: '',
      command: '',
    }, false));
  }

  public async onDeleteClick(entry: CronEntryViewModel): Promise<void> {
    this.entries = this.entries.filter(c => c.id !== entry.id);
  }

  public async onSaveClick(): Promise<void> {
    this.isSaving = true;
    // TODO
    this.isSaving = false;
  }

  private async refresh(): Promise<void> {
    this.isLoading = true;
    const odataPartitions = this.odata.partitions.entities();
    const partitions = await odataPartitions
      .get()
      .toPromise()
      .then(c => c.entities);
    this.entries = [];
    const crontab = await this.cronService.getAll();
    for (const entry of crontab) {
      const obj = new CronEntryViewModel(entry, true);
      obj.updateIsArmansMountingPoint(partitions);
      this.entries.push(obj);
    }

    this.isLoading = false;
  }

}

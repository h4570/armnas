import { Component, OnInit } from '@angular/core';
import { CronEntry } from 'src/app/models/os-commander/cron/cron-entry.model';
import { AppService } from 'src/app/services/app.service';
import { CronService } from 'src/app/services/cron.service';
import { smoothHeight } from '../../shared/animations';

@Component({
  selector: 'app-cron',
  templateUrl: './cron.component.html',
  styleUrls: ['./cron.component.scss'],
  animations: [smoothHeight]
})
export class CronComponent implements OnInit {

  public isSaving: boolean;
  public isLoading = true;

  public entries: CronEntry[] = [];

  constructor(
    public readonly appService: AppService,
    public readonly cronService: CronService,
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.refresh();
  }

  public async onAddClick(): Promise<void> {
    this.entries.unshift({
      cron: '',
      command: '',
    });
  }

  public async onDeleteClick(entry: CronEntry): Promise<void> {
    this.entries = this.entries.filter(c => c.command !== entry.command);
  }

  public async onSaveClick(): Promise<void> {
    this.isSaving = true;
    // TODO
    this.isSaving = false;
  }

  private async refresh(): Promise<void> {
    this.isLoading = true;
    this.entries = await this.cronService.getAll();
    this.isLoading = false;
  }

}

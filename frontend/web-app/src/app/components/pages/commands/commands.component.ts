import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { smoothHeight } from '../../shared/animations';
import { SambaEntryViewModel } from '../samba/view-models/samba-entry.view-model';

@Component({
  selector: 'app-commands',
  templateUrl: './commands.component.html',
  styleUrls: ['./commands.component.scss'],
  animations: [smoothHeight]
})
export class CommandsComponent implements OnInit {

  public isFreezed: boolean;
  public isSaving: boolean;
  public isLoading: boolean;

  public entries: SambaEntryViewModel[] = [];

  constructor(
    public readonly appService: AppService,
  ) {
    this.isFreezed = false;
  }

  public async ngOnInit(): Promise<void> {
    this.isLoading = true;
  }

  public async onAddClick(): Promise<void> {

  }

  public async onSaveClick(): Promise<void> {

  }

}

import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { SambaService } from 'src/app/services/samba.service';
import { smoothHeight } from '../../shared/animations';
import { SambaEntryViewModel } from './view-models/samba-entry.view-model';

@Component({
  selector: 'app-samba',
  templateUrl: './samba.component.html',
  styleUrls: ['./samba.component.scss'],
  animations: [smoothHeight]
})
export class SambaComponent implements OnInit {

  public entries: SambaEntryViewModel[] = [];
  public loading = true;
  public isSaving = false;

  public keySuggestions: string[] = [
    'comment',
    'browseable',
    'writable',
    'path',
    'guest ok',
    'log file',
    'server string',
    'workgroup',
    'max log size',
    'logging',
    'panic action',
    'server role',
    'obey pam restrictions',
    'unix password sync',
    'passwd program',
    'passwd chat',
    'pam password change',
    'map to guest',
    'usershare allow guests',
    'share modes',
    'printable',
    'read only',
    'public',
    'write list',
    'valid users',
    'printer',
    'create mask',
  ];

  constructor(
    private readonly fdService: FastDialogService,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
    private readonly snackbar: MatSnackBar,
    private readonly samba: SambaService,
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.refresh();
  }

  public async refresh(): Promise<void> {
    this.loading = true;
    this.entries = [];
    const res = await this.samba.getAll();
    res.forEach(entry => this.entries.push(new SambaEntryViewModel(entry)));
    this.loading = false;
  }

  public async onAddClick(): Promise<void> {
    const entry = new SambaEntryViewModel({ name: '', params: [] });
    this.entries.unshift(entry);
    entry.model.params.unshift({ key: 'path', value: '/mnt/armnas/share-name' });
    entry.model.params.unshift({ key: 'read only', value: 'no' });
    entry.model.params.unshift({ key: 'browsable', value: 'yes' });
  }

  public async onDeleteClick(entry: SambaEntryViewModel): Promise<void> {
    this.entries = this.entries.filter(c => c.id !== entry.id);
  }

  public async onAddParamClick(entry: SambaEntryViewModel): Promise<void> {
    entry.model.params.unshift({ key: '', value: '' });
  }

  public async onDeleteParamClick(entry: SambaEntryViewModel, param: { key: string; value: string }): Promise<void> {
    entry.model.params = entry.model.params.filter(c => !(c.key === param.key && c.value === param.value));
  }

  public async onSaveClick(): Promise<void> {
    this.isSaving = true;
    try {
      await this.samba.update(this.entries.map(c => c.model));
      this.snackbar.open('Done!', 'OK', { duration: 2000 });
      this.isSaving = false;
    } catch (err) {
      this.isSaving = false;
      this.snackbar.open('Saving failed!', '🙄', { duration: 2000 });
      await this.refresh();
    }
  }

}

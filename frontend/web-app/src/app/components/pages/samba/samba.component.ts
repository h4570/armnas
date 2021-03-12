import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { TranslateService } from '@ngx-translate/core';
import { SambaEntry } from 'src/app/models/os-commander/samba/samba-entry.model';
import { AppService } from 'src/app/services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { SambaService } from 'src/app/services/samba.service';
import { SambaEntryViewModel } from './view-models/samba-entry.view-model';

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-samba',
  templateUrl: './samba.component.html',
  styleUrls: ['./samba.component.scss']
})
export class SambaComponent implements OnInit {

  public entries: SambaEntryViewModel[] = [];
  public loading = true;

  constructor(
    private readonly fdService: FastDialogService,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
    private readonly samba: SambaService,
  ) { }

  public async ngOnInit(): Promise<void> {
    await this.refresh();
    // await this.samba.update(res);
  }

  public async refresh(): Promise<void> {
    this.loading = true;
    this.entries = [];
    const res = await this.samba.getAll();
    res.forEach(entry => this.entries.push(new SambaEntryViewModel(entry)));
    this.loading = false;
  }

}

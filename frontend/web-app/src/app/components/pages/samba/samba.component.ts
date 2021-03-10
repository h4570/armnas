import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { SambaService } from 'src/app/services/samba.service';

@Component({
  selector: 'app-samba',
  templateUrl: './samba.component.html',
  styleUrls: ['./samba.component.scss']
})
export class SambaComponent implements OnInit {

  constructor(
    private readonly fdService: FastDialogService,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
    private readonly samba: SambaService,
  ) { }

  public async ngOnInit(): Promise<void> {
    // const res = await this.samba.getAll();
    // await this.samba.update(res);
  }

}

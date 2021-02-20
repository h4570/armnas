import { Component, OnInit } from '@angular/core';
import { AppService } from '../../../services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  constructor(
    private readonly fdService: FastDialogService,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
  ) { }

  public async ngOnInit(): Promise<void> {

  }

}

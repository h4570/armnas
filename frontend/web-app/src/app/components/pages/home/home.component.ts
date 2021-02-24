import { Component, OnInit } from '@angular/core';
import { AppService } from '../../../services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { TranslateService } from '@ngx-translate/core';
import { smoothHeight } from '../../shared/animations';

export const REFRESH_OFF_VALUE = 10000;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [smoothHeight]
})

export class HomeComponent implements OnInit {

  public refreshInterval = 5000;
  public refreshOffValue = REFRESH_OFF_VALUE;

  constructor(
    private readonly fdService: FastDialogService,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
  ) { }

  public async ngOnInit(): Promise<void> {
    this.loadRefreshSettings();
  }

  public onRefreshIntervalChange(): void {
    this.saveRefreshSettings();
  }

  public formatRefreshIntervalLabel(value: number) {
    if (value === REFRESH_OFF_VALUE) return 'off';
    return (value / 1000).toFixed(1) + 's';
  }

  private loadRefreshSettings(): void {
    const refInterval = localStorage.getItem('sys-info-refresh-interval');
    if (refInterval)
      this.refreshInterval = parseInt(refInterval, 10);
  }

  private saveRefreshSettings(): void {
    localStorage.setItem('sys-info-refresh-interval', this.refreshInterval.toString());
  }

}

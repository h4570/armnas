import { Component, OnInit } from '@angular/core';
import { AppService } from '../../../services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { TranslateService } from '@ngx-translate/core';
import { smoothHeight } from '../../shared/animations';
import { PowerService } from 'src/app/services/power.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  public isFreezed = false;

  constructor(
    private readonly powerService: PowerService,
    private readonly snackbar: MatSnackBar,
    public readonly appService: AppService,
    private readonly translate: TranslateService,
  ) { }

  public async ngOnInit(): Promise<void> {
    this.loadRefreshSettings();
  }

  public async onPowerOffClick(): Promise<void> {
    this.isFreezed = true;
    try { // only that we can do :O
      await this.powerService.powerOff();
    } catch { }
    this.snackbar.open('Goodbye!', '😴', { duration: 15000 });
    this.isFreezed = false;
  }

  public async onRestartClick(): Promise<void> {
    this.isFreezed = true;
    try { // only that we can do :O
      await this.powerService.restart();
    } catch { }
    this.snackbar.open('See you in few minutes!', '😎', { duration: 15000 });
    this.isFreezed = false;
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

import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/services/app.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { TransmissionService } from 'src/app/services/transmission.service';
import { environment } from 'src/environments/environment';
import { smoothHeight } from '../../shared/animations';
import { DialogButtonType, DialogType } from '../../shared/fast-dialog/fast-dialog.component';

@Component({
  selector: 'app-transmission',
  templateUrl: './transmission.component.html',
  styleUrls: ['./transmission.component.scss'],
  animations: [smoothHeight]
})
export class TransmissionComponent implements OnInit {

  public incompletedDir = 'Loading...';
  public completedDir = 'Loading...';
  public readonly transmissionUrl: string;
  public isFreezed: boolean;

  constructor(
    public readonly appService: AppService,
    private readonly transmissionService: TransmissionService,
    private readonly snackbar: MatSnackBar,
    private readonly fastDialog: FastDialogService,
    public readonly translate: TranslateService,
  ) {
    this.transmissionUrl = environment.urls.transmission;
    this.isFreezed = false;
  }

  public async ngOnInit(): Promise<void> {
    await this.getLatestConfig();
  }

  public async onRestartClick(): Promise<void> {
    this.isFreezed = true;
    await this.restartTransmissionService();
    this.snackbar.open(this.translate.instant('common.done'), 'Ok!', { duration: 3000 });
    this.isFreezed = false;
  }

  public async onSaveClick(): Promise<void> {
    this.isFreezed = true;
    try {
      await this.transmissionService.updateConfig({ completedDir: this.completedDir, incompletedDir: this.incompletedDir });
      await this.restartTransmissionService();
      await this.getLatestConfig();
      this.snackbar.open(this.translate.instant('common.done'), 'Ok!', { duration: 3000 });
    } catch (raw) {
      const err = raw as HttpErrorResponse;
      const title = this.translate.instant('common.error') as string;
      const text = [this.translate.instant(err.error) as string];
      await this.fastDialog.open(DialogType.error, DialogButtonType.ok, title, text);
    }
    this.isFreezed = false;
  }

  private async restartTransmissionService(): Promise<void> {
    try {
      await this.transmissionService.restart();
    } catch (raw) {
      const err = raw as HttpErrorResponse;
      const title = this.translate.instant('common.error') as string;
      const text = [this.translate.instant(err.error) as string];
      await this.fastDialog.open(DialogType.error, DialogButtonType.ok, title, text);
    }
  }

  private async getLatestConfig(): Promise<void> {
    const res = await this.transmissionService.getConfig();
    this.completedDir = res.completedDir;
    this.incompletedDir = res.incompletedDir;
  }

}

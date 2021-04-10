import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { ServiceService } from 'src/app/services/service.service';
import { environment } from 'src/environments/environment';
import { DialogButtonType, DialogType } from '../../shared/fast-dialog/fast-dialog.component';

@Component({
  selector: 'app-transmission',
  templateUrl: './transmission.component.html',
  styleUrls: ['./transmission.component.scss']
})
export class TransmissionComponent {

  public readonly transmissionUrl: string;
  public isRestarting: boolean;

  constructor(
    private readonly serviceService: ServiceService,
    private readonly snackbar: MatSnackBar,
    private readonly fastDialog: FastDialogService,
    private readonly translate: TranslateService,
  ) {
    this.transmissionUrl = environment.urls.transmission;
    this.isRestarting = false;
  }

  public async onRestartClick(): Promise<void> {
    this.isRestarting = true;
    await this.restartTransmissionService();
    this.isRestarting = false;
  }

  private async restartTransmissionService(): Promise<void> {
    try {
      await this.serviceService.restart('transmission-daemon');
      this.snackbar.open('Done!', 'Ok!', { duration: 3000 });
    } catch (raw) {
      const err = raw as HttpErrorResponse;
      const title = this.translate.instant('common.error') as string;
      const text = [this.translate.instant(err.error) as string];
      await this.fastDialog.open(DialogType.error, DialogButtonType.ok, title, text);
    }
  }

}

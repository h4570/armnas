import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransmissionComponent } from './transmission.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SafePipe } from '../../shared/safe-pipe';
import { ServiceService } from 'src/app/services/service.service';
import { FastDialogService } from 'src/app/services/fast-dialog.service';

@NgModule({
  declarations: [
    TransmissionComponent,
    SafePipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TransmissionComponent
  ],
  providers: [
    MatSnackBar,
    FastDialogService,
    ServiceService
  ]
})
export class TransmissionModule { }

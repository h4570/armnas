import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransmissionComponent } from './transmission.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SafePipe } from '../../shared/safe-pipe';

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
    MatSnackBar
  ]
})
export class TransmissionModule { }

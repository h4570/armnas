import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransmissionComponent } from './transmission.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SafePipe } from '../../shared/safe-pipe';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { TransmissionService } from 'src/app/services/transmission.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@NgModule({
  declarations: [
    TransmissionComponent,
    SafePipe
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [
    TransmissionComponent
  ],
  providers: [
    MatSnackBar,
    FastDialogService,
    TransmissionService
  ]
})
export class TransmissionModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CronComponent } from './cron.component';
import { CronService } from 'src/app/services/cron.service';
import { SkeletonModule } from 'primeng/skeleton';
import { TranslateModule } from '@ngx-translate/core';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [
    CronComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    TranslateModule,
    MatInputModule,
    SkeletonModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [
    CronComponent
  ],
  providers: [
    MatSnackBar,
    FastDialogService,
    CronService
  ]
})
export class CronModule { }

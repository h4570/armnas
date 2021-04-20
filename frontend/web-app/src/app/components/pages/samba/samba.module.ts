import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SambaComponent } from './samba.component';
import { FormsModule } from '@angular/forms';
import { SambaService } from 'src/app/services/samba.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { SkeletonModule } from 'primeng/skeleton';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { SharedModule } from '../../shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    SambaComponent,
  ],
  imports: [
    CommonModule,
    MatIconModule,
    SharedModule,
    SkeletonModule,
    MatInputModule,
    TranslateModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatAutocompleteModule,
    MatButtonModule,
    FormsModule,
  ],
  exports: [
    SambaComponent
  ],
  providers: [
    SambaService,
    MatSnackBar,
  ]
})
export class SambaModule { }

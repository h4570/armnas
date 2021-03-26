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

@NgModule({
  declarations: [
    SambaComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    SkeletonModule,
    MatInputModule,
    MatAutocompleteModule,
    MatButtonModule,
    FormsModule,
  ],
  exports: [
    SambaComponent
  ],
  providers: [
    SambaService
  ]
})
export class SambaModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SambaComponent } from './samba.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SambaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
  ],
  exports: [
    SambaComponent
  ]
})
export class SambaModule { }

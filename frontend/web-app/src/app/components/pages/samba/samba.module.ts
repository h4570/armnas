import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SambaComponent } from './samba.component';
import { FormsModule } from '@angular/forms';
import { SambaService } from 'src/app/services/samba.service';

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
  ],
  providers: [
    SambaService
  ]
})
export class SambaModule { }

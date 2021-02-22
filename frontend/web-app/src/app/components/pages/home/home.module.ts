import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../../shared/shared.module';
import { SysInfoComponent } from './sys-info/sys-info.component';
import { SystemInformationService } from 'src/app/services/system-information.service';
import { SkeletonModule } from 'primeng/skeleton';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSliderModule } from '@angular/material/slider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
    declarations: [
        HomeComponent,
        SysInfoComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        SkeletonModule,
        RouterModule,
        TranslateModule,
        MatIconModule,
        MatButtonModule,
        MatProgressBarModule,
        SharedModule,
        MatSliderModule,
    ],
    exports: [
        HomeComponent
    ],
    providers: [
        SystemInformationService
    ]
})
export class HomeModule { }


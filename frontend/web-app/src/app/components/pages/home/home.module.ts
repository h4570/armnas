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
import { SmoothHeightAnimDirective } from '../../shared/smooth-height.directive';
import { DisksComponent } from './disks/disks.component';
import { SizePipe } from '../../shared/size-pipe';
import { MatInputModule } from '@angular/material/input';
import { ODataService } from 'src/app/services/odata.service';

@NgModule({
    declarations: [
        HomeComponent,
        SysInfoComponent,
        DisksComponent,
        SmoothHeightAnimDirective,
        SizePipe
    ],
    imports: [
        CommonModule,
        FormsModule,
        SkeletonModule,
        RouterModule,
        TranslateModule,
        MatInputModule,
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
        SystemInformationService,
        ODataService
    ]
})
export class HomeModule { }


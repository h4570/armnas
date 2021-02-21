import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../../shared/shared.module';
import { SysInfoComponent } from './sys-info/sys-info.component';
import { SystemInformationService } from 'src/app/services/system-information.service';

@NgModule({
    declarations: [
        HomeComponent,
        SysInfoComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        TranslateModule,
        SharedModule,
    ],
    exports: [
        HomeComponent
    ],
    providers: [
        SystemInformationService
    ]
})
export class HomeModule { }


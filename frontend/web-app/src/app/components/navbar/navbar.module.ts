import { NavbarComponent } from './navbar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
    declarations: [
        NavbarComponent
    ],
    imports: [
        CommonModule,
        TranslateModule,
        MatButtonModule,
        MatIconModule,
        MatBadgeModule,
        MatTooltipModule,
        MatMenuModule,
        RouterModule,
    ],
    exports: [
        NavbarComponent
    ],
    providers: [

    ],
    entryComponents: [

    ]
})
export class NavbarModule { }

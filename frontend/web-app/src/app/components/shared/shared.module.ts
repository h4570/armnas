import { ScreenLoaderComponent } from './screen-loader/screen-loader.component';
import { FastDialogComponent } from './fast-dialog/fast-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FastDialogService } from './../../services/fast-dialog.service';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SmoothHeightAnimDirective } from './smooth-height.directive';

@NgModule({
    declarations: [
        ScreenLoaderComponent,
        FastDialogComponent,
        SmoothHeightAnimDirective,
    ],
    imports: [
        TranslateModule,
        CommonModule,
        MatProgressBarModule,
        MatBottomSheetModule,
        MatDialogModule,
        MatButtonModule,
        MatIconModule,
    ],
    exports: [
        ScreenLoaderComponent,
        SmoothHeightAnimDirective
    ],
    providers: [
        FastDialogService
    ],
    entryComponents: [
        FastDialogComponent
    ]
})
export class SharedModule { }

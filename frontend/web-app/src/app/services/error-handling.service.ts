import { Injectable, ErrorHandler } from '@angular/core';
import { FastDialogService } from './fast-dialog.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogType, DialogButtonType } from '../components/shared/fast-dialog/fast-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class ErrorHandlingService extends ErrorHandler {

    constructor(
        private readonly fastDialog: FastDialogService,
        private readonly translate: TranslateService
    ) {
        super();
    }

    public handleError(error: Error): void {
        if (environment.name === 'dev')
            console.error(error);
        // else // TODO, log to external website
    }

    /** Handle error 500 (server error), 0 (timeout) and 401 (unauthorized) with fast dialog */
    public handleHttpError(err: HttpErrorResponse): Promise<HttpErrorResponse> {
        let title: string;
        let texts: string[];
        let type: DialogType;
        let isError = false;
        if (err.status === 0) {
            isError = true;
            title = this.translate.instant('coreErrors.error0Title') as string;
            texts = [this.translate.instant('coreErrors.error0Subtitle') as string];
            type = DialogType.error;
            // TODO, log to external website new Error(JSON.stringify(err))
        }
        else if (err.status === 500) {
            isError = true;
            console.error(err);
            title = this.translate.instant('coreErrors.error500Title') as string;
            texts = [this.translate.instant('coreErrors.error500Subtitle') as string, `${err.error}`.split('\n')[0]];
            type = DialogType.error;
            // TODO, log to external website new Error(JSON.stringify(err))
        }
        else if (err.status === 401) {
            isError = true;
            title = this.translate.instant('coreErrors.error401Title') as string;
            texts = [this.translate.instant('coreErrors.error401Subtitle') as string];
            type = DialogType.alert;
            // TODO, log to external website new Error(JSON.stringify(err))
        }
        return new Promise<HttpErrorResponse>(async (res, rej) => {
            if (isError) {
                await this.fastDialog.open(type, DialogButtonType.ok, title, texts);
                res(err);
            } else res(err);
        });
    }

}

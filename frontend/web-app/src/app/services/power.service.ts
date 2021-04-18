import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class PowerService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async powerOff(): Promise<void> {
        const uri = `power/off`;
        return this.http
            .get<any>(`${environment.urls.api}` + uri)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async restart(): Promise<void> {
        const uri = `power/restart`;
        return this.http
            .get<any>(`${environment.urls.api}` + uri)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

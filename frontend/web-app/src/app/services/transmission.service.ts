import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TransmissionConfig } from '../models/os-commander/transmission/transmission-config.model';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class TransmissionService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async restart(): Promise<void> {
        const uri = `transmission/restart`;
        return this.http
            .get<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async getConfig(): Promise<TransmissionConfig> {
        const uri = `transmission/config`;
        return this.http
            .get<TransmissionConfig>(`${environment.urls.api}` + uri)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async updateConfig(payload: TransmissionConfig): Promise<void> {
        const uri = `transmission/config`;
        return this.http
            .patch<void>(`${environment.urls.api}` + uri, payload)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }


}

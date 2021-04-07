import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class ServiceService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async start(name: string): Promise<void> {
        const uri = `service/start/${encodeURIComponent(name)}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async stop(name: string): Promise<void> {
        const uri = `service/stop/${encodeURIComponent(name)}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async restart(name: string): Promise<void> {
        const uri = `service/restart/${encodeURIComponent(name)}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class PartitionService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async mount(uuid: string): Promise<void> {
        const uri = `partition/mount/${uuid}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async unmount(uuid: string): Promise<void> {
        const uri = `partition/unmount/${uuid}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async checkAutoMount(uuid: string): Promise<boolean> {
        const uri = `partition/check-auto-mount/${uuid}`;
        return this.http
            .get<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .then((c: { message: boolean }) => c.message)
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async enableAutoMount(uuid: string): Promise<void> {
        const uri = `partition/enable-auto-mount/${uuid}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async disableAutoMount(uuid: string): Promise<void> {
        const uri = `partition/disable-auto-mount/${uuid}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, {})
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SambaEntry } from '../models/os-commander/samba/samba-entry.model';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class SambaService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async getAll(): Promise<SambaEntry[]> {
        const uri = `samba`;
        return this.http
            .get<SambaEntry[]>(`${environment.urls.api}` + uri)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

    public async update(payload: SambaEntry[]): Promise<void> {
        const uri = `samba`;
        return this.http
            .post<void>(`${environment.urls.api}` + uri, payload)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }


}

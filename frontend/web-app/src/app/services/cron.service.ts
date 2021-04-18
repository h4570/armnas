import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CronEntry } from '../models/os-commander/cron/cron-entry.model';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class CronService {

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async getAll(): Promise<CronEntry[]> {
        const uri = `cron`;
        return this.http
            .get<CronEntry[]>(`${environment.urls.api}` + uri)
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

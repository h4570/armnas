import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class SystemInformationService {

    private url = `${environment.urls.api}system-information`;

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) { }

    public async getDistributionName(): Promise<string> {
        console.log(`${this.url}/distribution`);
        return this.http
            .get(`${this.url}/distribution`, { responseType: 'text' })
            .toPromise()
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

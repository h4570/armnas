import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AppHistoryAdapter } from '../adapters/app-history.adapter';
import { AppHistory } from '../models/app-history.model';
import { AppTable } from '../models/app-table.enum';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class AppHistoryService {

    private readonly historyAdapter: AppHistoryAdapter;

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) {
        this.historyAdapter = new AppHistoryAdapter();
    }

    public async get(tableId: AppTable, elementId: number = null): Promise<AppHistory[]> {
        const uri = elementId
            ? `app-history/table/${tableId}/element/${elementId}`
            : `app-history/table/${tableId}`;
        return this.http
            .get<any[]>(`${environment.urls.api}` + uri)
            .toPromise()
            .then(raws => this.historyAdapter.adaptMany(raws))
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

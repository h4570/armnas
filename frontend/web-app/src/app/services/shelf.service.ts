import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ShelfAdapter } from '../adapters/shelf.adapter';
import { ShelfCategoryAdapter } from '../adapters/shelf-category.adapter';
import { Shelf } from '../models/shelf.model';
import { ErrorHandlingService } from './error-handling.service';

@Injectable()
export class ShelfService {

    private readonly shelfAdapter: ShelfAdapter;

    constructor(
        private readonly http: HttpClient,
        private readonly errHandler: ErrorHandlingService
    ) {
        const categoryAdapter = new ShelfCategoryAdapter();
        this.shelfAdapter = new ShelfAdapter(categoryAdapter);
    }

    public async getAll(): Promise<Shelf[]> {
        return this.http
            .get<any[]>(`${environment.urls.api}shelfs`)
            .toPromise()
            .then(raws => this.shelfAdapter.adaptMany(raws))
            .catch(async (err: HttpErrorResponse) => { throw await this.errHandler.handleHttpError(err); });
    }

}

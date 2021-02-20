import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject } from 'rxjs';
import { PagedResult } from './paged-result';
import { LazyService } from './lazy-service';

export class LazyDataSource<T> implements DataSource<T> {

    private dataSubject = new BehaviorSubject<T[]>([]);

    constructor(private lazyService: LazyService<T>) { }

    public connect(collectionViewer: CollectionViewer): Observable<T[]> {
        return this.dataSubject.asObservable();
    }

    public disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
    }

    public async reload(pageIndex: number, pageSize: number, filter: string): Promise<PagedResult<T>> {
        return new Promise<any>((res, rej) => {
            this.lazyService.lazyDataFunction(pageIndex, pageSize, filter)
                .then(pagedResult => {
                    this.dataSubject.next(pagedResult.results);
                    res(pagedResult);
                })
                .catch((err) => {
                    console.error(err);
                    res(err);
                });
        });

    }

}

import { PagedResult } from './paged-result';

export type LazyEndpoint<T> = (pageIndex: number, pageSize: number, filter: string) => Promise<PagedResult<T>>;

export interface LazyService<T> {
    lazyDataFunction: LazyEndpoint<T>;
}

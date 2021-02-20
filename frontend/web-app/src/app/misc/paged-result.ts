export abstract class PagedResultBase {
    public currentPage = 0;
    public pageCount = 0;
    public pageSize = 0;
    public rowCount = 0;
    public firstRowOnPage = 0;
    public lastRowOnPage = 0;
}

export abstract class PagedResult<T> extends PagedResultBase {
    public results: T[] = [];
}

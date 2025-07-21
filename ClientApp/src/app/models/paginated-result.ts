export class PaginatedResult<T> {
    data: Array<T>;
    totalCount: number;
    pageNumber: number;
    pageSize: number;

    constructor(data?: Array<T>, totalCount?: number, pageNumber?: number, pageSize?: number) {
        this.data = data ? data : [];
        this.totalCount = totalCount ? totalCount : 0;
        this.pageNumber = pageNumber ? pageNumber : 1;
        this.pageSize = pageSize ? pageSize : 10;
    }

    get totalPages(): number {
        return Math.ceil(this.totalCount / this.pageSize);
    }
}
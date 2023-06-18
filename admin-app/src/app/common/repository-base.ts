import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { retry, catchError } from "rxjs/operators";

export interface IRepository<T> {
    get(pageIndex: number, pageSize: number, filter: string, sortOrder: string): Observable<T[]>;
    getAll(): Observable<T[]>;
    getTotal(): Observable<number>;
    getSingle(id: number): Observable<T>;
    save(item: T): Observable<number>;
    add(item: T): Observable<number>;
}

export class Repository<T> implements IRepository<T>{
    constructor(http: HttpClient, api: string) {
        this.http = http;
        this.api = `api/${api}`;
    }

    public http: HttpClient
    private api: string;

    get(pageIndex: number, pageSize: number, filter: string, sortOrder: string): Observable<T[]> {
        var params: HttpParams = new HttpParams()
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString())
            .set("filter", filter)
            .set("sortOrder", sortOrder);

        return this.http
            .get<T[]>(this.api, {
                params: params
            })
            .pipe(
                retry(3),
                catchError((err) => of([]))
            );
    }

    getAll(): Observable<T[]> {
        return this.http
            .get<T[]>(`${this.api}/all`)
            .pipe(
                retry(3),
                catchError((err) => of([]))
            );
    }

    getTotal(): Observable<number> {
        return this.http
            .get<number>(`${this.api}/total`);
    }

    getSingle(id: number): Observable<T> {
        return this.http
            .get<T>(`${this.api}/${id}`);
    }

    save(item: T): Observable<number> {
        return this.http.put<number>(this.api, item);
    }

    add(item: T): Observable<number> {
        return this.http.post<number>(this.api, item);
    }

    remove(id: number): Observable<any> {
        return this.http.delete(`${this.api}/${id}`);
    }
}
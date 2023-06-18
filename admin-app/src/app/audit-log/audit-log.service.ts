import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of, BehaviorSubject } from "rxjs";
import { retry, catchError } from "rxjs/operators";
import { DataSource } from "@angular/cdk/table";
import { CollectionViewer } from "@angular/cdk/collections";
import { Repository } from "../common/repository-base";

@Injectable({
    providedIn: "root"
})
export class AuditLogService extends Repository<IAuditLog> {
    constructor(public http: HttpClient) {
        super(http, 'auditlog')
    }
}

export interface IAuditLog {
    id: number;
    user: number;
    date: Date;
    data: string;
}

export class AuditLogDataSource extends DataSource<IAuditLog> {
    constructor(
        private settingService: AuditLogService,
        initialValues?: IAuditLog[]
    ) {
        super();

        this.auditLogSubject = new BehaviorSubject(initialValues);
    }

    private auditLogSubject: BehaviorSubject<IAuditLog[]>;

    connect(collectionViewer: CollectionViewer): Observable<IAuditLog[] | readonly IAuditLog[]> {
        return this.auditLogSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.auditLogSubject.complete();
    }

    load(pageIndex: number, pageSize: number, filter: string, sortOrder: string) {
        this.settingService
            .get(pageIndex, pageSize, filter, sortOrder)
            .pipe(
                retry(3),
                catchError((err) => of([]))
            )
            .subscribe((res) => this.auditLogSubject.next(res));
    }
}
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError, retry, finalize, map, refCount, publishReplay } from "rxjs/operators";
import { of, BehaviorSubject, Observable } from "rxjs";
import { DataSource } from "@angular/cdk/table";
import { CollectionViewer } from "@angular/cdk/collections";
import { ISetting } from "../settings/setting.service";
import { Repository } from "../common/repository-base";

@Injectable({
    providedIn: "root"
})
export class AttorneyService extends Repository<IAttorney> {

    constructor(public http: HttpClient) {
        super(http, 'attorneys')
    }

    deactivate(id: number): Observable<Object> {
        return this.http.post(`api/attorneys/deactivate/${id}`, null);
    }
}

export interface IAttorney {
    id: number;
    name: string;
    kref: string;
    lun: string;
    altlun: string;
    debtorCode: string;
    branch: string;
    onlineActivationChecked: boolean;
    onlineActivationDate: Date;
    isHostedFirm: boolean;
    settings: ISetting[]
}

export class AttorneysDataSource extends DataSource<IAttorney> {
    private attorneysSubject: BehaviorSubject<IAttorney[]>;
    private loadingSubject: BehaviorSubject<boolean>;
    loading$: Observable<boolean>;

    constructor(
        private attorneyService: AttorneyService,
        initialValues?: IAttorney[]
    ) {
        super();

        this.attorneysSubject = new BehaviorSubject(initialValues);

        this.loadingSubject = new BehaviorSubject(false);
        this.loading$ = this.loadingSubject.asObservable()
    }

    connect(collectionViewer: CollectionViewer): Observable<IAttorney[] | readonly IAttorney[]> {
        return this.attorneysSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.attorneysSubject.complete();
    }

    load(pageIndex: number, pageSize: number, searchText: string, sortOrder: string = "asc"): void {
        this.loadingSubject.next(true);
        this.attorneyService
            .get(pageIndex, pageSize, searchText, sortOrder)
            .pipe(
                retry(3),
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(res => this.attorneysSubject.next(res));
    }
}

export interface IAttorneyDialogData {
    id: number;
}
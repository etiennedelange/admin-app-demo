import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, of, BehaviorSubject } from "rxjs";
import { retry, catchError, finalize } from "rxjs/operators";
import { DataSource } from "@angular/cdk/table";
import { CollectionViewer } from "@angular/cdk/collections";
import { Repository } from "../common/repository-base";
import { basename } from "path";

@Injectable({
    providedIn: "root"
})
export class SettingService extends Repository<ISetting> {
    constructor(public http: HttpClient) {
        super(http, 'settings')
    }
}

export interface ISetting {
    id: number;
    guid: string;
    description: string;
    enabledGlobally: boolean;
    enabled: boolean;
}

export class SettingsDataSource extends DataSource<ISetting> {
    constructor(
        private settingService: SettingService,
        initialValues?: ISetting[]
    ) {
        super();

        this.settingsSubject = new BehaviorSubject(initialValues);
        this.loadingSubject = new BehaviorSubject(false);
        this.loading$ = this.loadingSubject.asObservable();
    }

    private settingsSubject: BehaviorSubject<ISetting[]>;
    private loadingSubject: BehaviorSubject<boolean>;
    public loading$: Observable<boolean>;

    connect(collectionViewer: CollectionViewer): Observable<ISetting[] | readonly ISetting[]> {
        return this.settingsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.settingsSubject.complete();
    }

    load(pageIndex: number, pageSize: number, filter: string, sortOrder: string) {
        this.loadingSubject.next(true);
        this.settingService
            .get(pageIndex, pageSize, filter, sortOrder)
            .pipe(
                retry(3),
                catchError((err) => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe((res) => this.settingsSubject.next(res));
    }
}

export interface ISettingDialogData {
    id: number;
}
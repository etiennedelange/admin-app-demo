import { CollectionViewer } from '@angular/cdk/collections';
import { DataSource } from '@angular/cdk/table';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { retry, catchError, finalize } from 'rxjs/operators';
import { Repository } from '../common/repository-base';
import { SettingService, ISetting } from '../settings/setting.service';
import { IProductVersion } from '../templates/template.service';

@Injectable({
    providedIn: 'root'
})
export abstract class TemplateVersionService extends Repository<IProductVersion>{
    constructor(public http: HttpClient, api: string) {
        super(http, api);
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateDesktopVersionService extends TemplateVersionService {
    constructor(public http: HttpClient) {
        super(http, 'desktopproductversions');
    }
}

@Injectable({
    providedIn: 'root'
})
export class TemplateOnlineVersionService extends TemplateVersionService {
    constructor(public http: HttpClient) {
        super(http, 'onlineproductversions');
    }
}

export class TemplateVersionsDataSource extends DataSource<IProductVersion> {
    constructor(
        public service: TemplateVersionService,
        initialValues?: IProductVersion[]
    ) {
        super();

        this.templateVersionsSubject = new BehaviorSubject(initialValues);
    }

    getValue() {
        return this.templateVersionsSubject.getValue();
    }

    private templateVersionsSubject: BehaviorSubject<IProductVersion[]>;

    connect(collectionViewer: CollectionViewer): Observable<IProductVersion[]> {
        return this.templateVersionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.templateVersionsSubject.complete();
    }

    load(pageIndex: number, pageSize: number, filter: string, sortOrder: string) {
        this.service
            .get(pageIndex, pageSize, filter, sortOrder)
            .pipe(
                retry(3),
                catchError((err) => of([]))
            )
            .subscribe((res) => this.templateVersionsSubject.next(res));
    }
}
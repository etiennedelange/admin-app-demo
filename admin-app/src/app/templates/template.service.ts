import { CollectionViewer } from '@angular/cdk/collections';
import { DataSource } from '@angular/cdk/table';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Repository } from '../common/repository-base';

@Injectable({
    providedIn: 'root'
})
export class TemplateService extends Repository<ITemplate> {

    constructor(
        public http: HttpClient
    ) {
        super(http, 'templates');
    }

    addTemplate(template: ITemplate, file: File): Observable<string> {
        const formData = this.buildFormData(file, template);

        return this.http.post<string>('api/templates/', formData);
    }

    saveWithTemplate(template: ITemplate, file: File): Observable<string> {
        const formData = this.buildFormData(file, template);

        return this.http.put('api/templates/savewithtemplate', formData, {
            responseType: 'text'
        });
    }

    queueDownload(guid: string, contentHash: string): Observable<any> {
        return this.http.post('api/templates/queueDownload', null, {
            params: new HttpParams()
                .set("guid", guid)
                .set("contentHash", contentHash)
        });
    }

    private buildFormData(file: File, template: ITemplate) {
        const formData = new FormData();
        formData.append('data', file, file.name);
        // Append model as json string so that both file and model data can be sent to server with one call
        formData.append('template', JSON.stringify(template));
        return formData;
    }
}

export interface ITemplate {
    id: number;
    guid: string;
    description: string;
    available: boolean;
    templateContentHash: string;
    desktopProductVersions: IProductVersion[];
    onlineProductVersions: IProductVersion[];
}

export interface IProductVersion {
    id: number;
    versionNumber: string;
    releaseDate: Date;
}

export class TemplatesDataSource extends DataSource<ITemplate> {
    private templatesSubject: BehaviorSubject<ITemplate[]>;

    constructor(
        private templateService: TemplateService,
        initialValues?: ITemplate[]
    ) {
        super();
        this.templatesSubject = new BehaviorSubject(initialValues);
    }

    connect(collectionViewer: CollectionViewer): Observable<ITemplate[] | readonly ITemplate[]> {
        return this.templatesSubject.asObservable();
    }
    disconnect(collectionViewer: CollectionViewer): void {
        this.templatesSubject.complete();
    }

    load(pageIndex: number, pageSize: number, filter: string, sortOrder: string) {
        this.templateService
            .get(pageIndex, pageSize, filter, sortOrder)
            .pipe(
                retry(3),
                catchError(() => of([])),
            )
            .subscribe((res) => this.templatesSubject.next(res));
    }
}

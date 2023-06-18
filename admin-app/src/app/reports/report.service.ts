import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ReportService {

    constructor(private http: HttpClient) {
    }

    download(name: string): Observable<any> {
        return this.http.get(`api/reports/${name}`, { responseType: 'blob' });
    }
}

export class Report {
    public name: string;
    public displayName: string;
}
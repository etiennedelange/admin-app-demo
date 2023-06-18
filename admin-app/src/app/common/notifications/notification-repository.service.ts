import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Repository } from "../repository-base";

@Injectable({
    providedIn: 'root'
})
export class NotificationRepository extends Repository<INotification> {
    constructor(
        public http: HttpClient
    ) {
        super(http, 'notifications');
    }

    removeAll(): Observable<any> {
        return this.http.post(`api/notifications/removeAll`, null);
    }
}

export interface INotification {
    id?: number;
    message: string;
    read: boolean;
    dateReceived: Date;

    templateId?: number;
    templateContentHash?: string;
    templateGuid?: string;
}
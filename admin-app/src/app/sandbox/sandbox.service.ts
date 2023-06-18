import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { INotification } from "../common/notifications/notification-repository.service";

@Injectable({
    providedIn: "root"
})
export class SandboxService {
    constructor(private http: HttpClient) {
    }

    postNotificationToHub(notification: INotification): Observable<any> {
        return this.http.post('api/sandbox/postNotificationToHub', notification)
    }
}
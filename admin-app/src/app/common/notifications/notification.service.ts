import { Injectable, OnInit } from "@angular/core";
import { AsyncSubject, Observable, Subject } from "rxjs";
import { SnackBarService } from "src/app/shared/services/snack-bar.service";
import { INotification, NotificationRepository } from "./notification-repository.service";

@Injectable({
    providedIn: "root"
})
export class NotificationService implements OnInit {
    constructor(
        private notificationRepository: NotificationRepository,
        private snack: SnackBarService) {
        this.notificationSubject = new Subject<INotification[]>();
        this.notificationsClearedSubject = new Subject();
    }

    public notificationSubject: Subject<INotification[]>;
    public notificationsClearedSubject: Subject<boolean>;

    ngOnInit(): void {
    }

    public notification(): Observable<INotification[]> {
        return this.notificationSubject.asObservable();
    }

    public notificationsCleared(): Observable<boolean> {
        return this.notificationsClearedSubject.asObservable();
    }

    public notify(notification: INotification): void {
        this.notifyMultiple([notification]);
    }

    public notifyMultiple(notification: INotification[]): void {
        this.notificationSubject.next(notification);
    }

    public clear(): void {
        this.notificationRepository
            .removeAll()
            .subscribe({
                next: () => this.notificationsClearedSubject.next(true),
                error: (error) => this.snack.open({ message: "There was an error clearing all notifications" })
            });
    }
}
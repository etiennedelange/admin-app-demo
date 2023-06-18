import { HttpErrorResponse } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { SnackBarService } from "src/app/shared/services/snack-bar.service";
import { INotification } from "./notification-repository.service";
import { NotificationService } from "./notification.service";
import { Location } from '@angular/common';
import { ToastrService } from "ngx-toastr";
import { take } from "rxjs/operators";
import { SidenavService } from "../sidenav.service";

@Injectable({
    providedIn: "root"
})
export class NotificationHubClientService {
    constructor(
        private authenticationService: AuthenticationService,
        private notificationService: NotificationService,
        private toastr: ToastrService,
        private sidenav: SidenavService) {
    }

    private connection: HubConnection;

    public start(): void {
        if (!this.authenticationService.isLoggedIn()) {
            return;
        }

        if (this.connection?.state == HubConnectionState.Connected) {
            return;
        }

        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Debug)
            .withAutomaticReconnect()

            .withUrl(`${window.location.origin}/signalRNotifications`, {
                accessTokenFactory: () => this.authenticationService.getToken()
            })
            .build();

        this.connection.start()
            .then(() => console.debug('NotificationHubClientService SignalR client Connected'))
            .catch((err: HttpErrorResponse) => console.error(err.error));

        this.connection.on("Notify", (notification: INotification) => {
            this.notificationService.notify(notification);

            if (!this.sidenav.isRightSidenavOpen()) {
                this.toastr.info(notification.message, null)
                    .onTap
                    .pipe(take(1))
                    .subscribe(() => this.sidenav.toggleRightSidenav(true));
            }
        });
    }

    public stop(): void {
        if (this.connection) {
            this.connection.stop()
            this.connection = undefined;
        }
    }
}
import { Component, Input, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { INotification } from "../common/notifications/notification-repository.service";
import { NotificationService } from "../common/notifications/notification.service";
import { SidenavService } from "../common/sidenav.service";
import { AuthenticationService, IAuthenticatedUser } from "../shared/services/authentication.service";
import { UserRole } from "../shared/user-role";

@Component({
    selector: "app-header",
    templateUrl: "./app-header.component.html",
    styleUrls: ['./app-header.component.less']
})
export class AppHeaderComponent implements OnInit {
    @Input() private username: string;

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router,
        private notificationService: NotificationService,
        private sidenavService: SidenavService) {
        this.username = this.authenticationService.getUsername();

        this.authenticationService.authenticationChanged()
            .subscribe((result: IAuthenticatedUser) => {
                this.username = result.username;
            });
    }

    notificationCount: number = 1;
    notifications: INotification[] = [];

    ngOnInit() {
        this.notificationService.notification().subscribe((notifications) => this.notifications.push(...notifications));
        this.notificationService.notificationsCleared().subscribe(() => this.notifications = []);

        this.authenticationService.loginStateChanged().subscribe((loggedIn: boolean) => {
            if (!loggedIn) {
                this.notifications = [];
            }
        });
    }

    public logout(): void {
        this.authenticationService.logout();
    }

    public isLoggedIn(): boolean {
        return this.authenticationService.isLoggedIn();
    }

    public navigateToHome() {
        this.router.navigate(['/home']);
    }

    public openSidenav() {
        this.sidenavService.toggleRightSidenav();
    }

    public userIsAdministrator(): boolean {
        return this.authenticationService.hasRole(UserRole.Administrator);
    }
}
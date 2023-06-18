import { Component, OnInit, ViewChild, AfterViewInit, HostListener } from '@angular/core';
import { AuthenticationService } from './shared/services/authentication.service';
import { fadeAnimation } from './animations';
import { Router, RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { NotificationHubClientService } from './common/notifications/notification-hub-client.service';
import { MatSidenav } from '@angular/material/sidenav';
import { SidenavService } from './common/sidenav.service';
import { NotificationRepository } from './common/notifications/notification-repository.service';
import { NotificationService } from './common/notifications/notification.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.less'],
    animations: [fadeAnimation]
})
export class AppComponent implements OnInit, AfterViewInit {
    constructor(
        private authenticationService: AuthenticationService,
        private notificationHub: NotificationHubClientService,
        private notificationService: NotificationService,
        private sidenavService: SidenavService,
        private notificationRepository: NotificationRepository,
        private router: Router) {
    }

    @ViewChild('sidenavRight') sidenavRight: MatSidenav;
    @ViewChild('sidenavLeft') sidenavLeft: MatSidenav;

    @HostListener('document:keydown.control.alt.d', ['$event']) onKeydownHandler(e: KeyboardEvent) {
        this.router.navigate(['/sandbox']);
    }

    private notifications: Notification[] = [];
    private subscription: Subscription;

    ngOnInit(): void {
        this.notificationHub.start();

        this.authenticationService.loginStateChanged()
            .subscribe((loggedIn: boolean) => {
                if (loggedIn) {
                    this.notificationHub.start();
                    this.loadNotifications();
                } else {
                    this.notificationHub.stop();
                }
            });

        this.loadNotifications();
    }

    ngAfterViewInit(): void {
        this.sidenavService.setRightSidenav(this.sidenavRight);
        this.sidenavService.setLeftSidenav(this.sidenavLeft);

        if (this.isLoggedIn()) {
            // Causes error without setTimeout: NG0100: ExpressionChangedAfterItHasBeenCheckedError: Expression has changed after it was checked. Previous value for '@transform': 'void'. Current value: 'open'.. Find more at https://angular.io/errors/NG0100
            setTimeout(() => this.sidenavService.toggleLastKnownStateForRightSidenav())
        }
    }

    private loadNotifications() {
        if (!this.isLoggedIn()) {
            return
        }

        this.notificationRepository
            .getAll()
            .subscribe({
                next: (result) => {
                    if (result?.length > 0) {
                        this.notificationService.notifyMultiple(result)
                    }
                },
            });
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    public isLoggedIn(): boolean {
        return this.authenticationService.isLoggedIn();
    }

    public getAnimationData(outlet: RouterOutlet) {
        return outlet && outlet.activatedRouteData;
    }
}
import { trigger, transition, style, animate, stagger, query } from '@angular/animations';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { TemplateService } from 'src/app/templates/template.service';
import { INotification, NotificationRepository } from '../notifications/notification-repository.service';
import { NotificationService } from '../notifications/notification.service';
import { SidenavService } from '../sidenav.service';

@Component({
    selector: 'app-sidenav',
    templateUrl: './app-sidenav.component.html',
    styleUrls: ['./app-sidenav.component.less'],
    animations: [
        trigger('fade', [
            transition('* <=> *', [
                query(':enter',
                    [style({ opacity: '0' }), stagger('60ms', animate('.2s ease-in', style({ opacity: '1' })))],
                    { optional: true }),
                query(':leave',
                    [style({ opacity: '1' }), stagger('25ms', animate('.2s ease-out', style({ opacity: '0' }))),
                    ], { optional: true }),
            ])])
    ]
})
export class AppSidenavComponent implements OnInit {
    constructor(
        private notificationService: NotificationService,
        private templateService: TemplateService,
        private authenticationService: AuthenticationService,
        private dialogService: DialogService
    ) { }

    notifications: INotification[] = [];

    ngOnInit(): void {
        this.notificationService.notification().subscribe((notifications) => this.notifications.push(...notifications));
        this.notificationService.notificationsCleared().subscribe(() => this.notifications = []);

        this.authenticationService.loginStateChanged().subscribe((loggedIn: boolean) => {
            if (!loggedIn) {
                this.notifications = [];
            }
        });
    }

    public retry(item: INotification) {
        this.templateService
            .queueDownload(item.templateGuid, item.templateContentHash)
            .subscribe();
    }

    public getSortedNotifications(): INotification[] {
        // not working correctly
        this.notifications = this.notifications.sort((a, b) => a.id - b.id);
        return this.notifications;
    }

    public clearAllNotifications() {
        this.notificationService.clear();
    }

    public openNotification(item: INotification) {
        this.dialogService.showConfirmationDialog({
            title: 'Notification',
            message: item.message,
            showCancel: false
        });
    }
}

import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { NotificationService } from '../common/notifications/notification.service';
import { SidenavService } from '../common/sidenav.service';
import { SandboxService } from './sandbox.service';

@Component({
    selector: 'app-sandbox',
    templateUrl: './sandbox.component.html',
    styleUrls: ['./sandbox.component.less']
})
export class SandboxComponent implements OnInit {

    constructor(
        private notificationService: NotificationService,
        private toastr: ToastrService,
        private sidenavService: SidenavService,
        private sandbox: SandboxService
    ) { }

    ngOnInit(): void {
    }

    addNotification(value: string) {
        this.notificationService.notify({ message: value, dateReceived: new Date(), read: false });
    }

    showToast(value: string) {
        this.toastr.info(value, null, {
            tapToDismiss: true,
        }).onTap
            .pipe(take(1))
            .subscribe(() => this.sidenavService.toggleRightSidenav(true));
    }

    postNotificationToHub(value: string) {
        this.sandbox.postNotificationToHub({
            message: value,
            dateReceived: new Date(),
            read: false
        }).subscribe();
    }
}

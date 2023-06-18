import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmationDialogComponent, IConfirmationDialogData } from 'src/app/common/confirmation-dialog/confirmation-dialog.component';

@Injectable({
    providedIn: 'root'
})
export class DialogService {
    constructor(
        private dialog: MatDialog
    ) { }

    public showConfirmationDialog({ title, message, showCancel = true }: IConfirmationDialogData): Observable<boolean> {
        let dialogRef = this.dialog.open<ConfirmationDialogComponent, IConfirmationDialogData, boolean>(ConfirmationDialogComponent, {
            data: {
                title: title,
                message: message,
                showCancel: showCancel
            }
        });
        return dialogRef.afterClosed();
    }
}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    templateUrl: './confirmation-dialog.component.html'
})
export class ConfirmationDialogComponent implements OnInit {
    constructor(
        private dialogRef: MatDialogRef<ConfirmationDialogComponent, boolean>,
        @Inject(MAT_DIALOG_DATA) public data: IConfirmationDialogData) {
    }

    ngOnInit(): void {
    }

    okay() {
        this.dialogRef.close(true);
    }

    cancel() {
        this.dialogRef.close(false);
    }
}

export interface IConfirmationDialogData {
    title: string;
    message: string;
    showCancel?: boolean;
}

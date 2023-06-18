import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-file-upload-dialog',
    templateUrl: './file-upload-dialog.component.html'
})
export class FileUploadDialogComponent implements OnInit {
    constructor(
        public dialogRef: MatDialogRef<FileUploadDialogComponent>,
    ) { }

    droppedFile: File;

    ngOnInit(): void {
    }

    public setDroppedFile(file: File) {
        this.droppedFile = file;
    }

    cancel() {
        this.dialogRef.close();
    }

    okay() {
        this.dialogRef.close(this.droppedFile);
    }
}

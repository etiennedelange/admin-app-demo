import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { FileUploadDialogComponent } from 'src/app/templates/file-upload-dialog/file-upload-dialog.component';

@Component({
    selector: 'app-drag-and-drop',
    templateUrl: './app-drag-and-drop.component.html',
    styleUrls: ['./app-drag-and-drop.component.less']
})
export class DragAndDropComponent implements OnInit {
    constructor(private dialog: MatDialog) { }

    ngOnInit(): void { }

    @Output() onDropped: EventEmitter<File> = new EventEmitter();

    @Input() multiple: boolean = false;
    @Input() disabled: boolean = false;
    @Input() uploadedText: string = "File ready for upload";
    @Input() text: string = "Drag a file here";

    public files: NgxFileDropEntry[] = [];

    fileDropped: boolean = false;

    public dropped(files: NgxFileDropEntry[]) {
        this.files = files;

        //let filesToUpload: File[] = [];
        for (const droppedFile of files) {

            if (droppedFile.fileEntry.isFile) {
                const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;

                //TODO: Fix Causes issues with drag and drop, file promise gets called after event emitted?!
                fileEntry.file((file: File) => {
                    this.onDropped.emit(file);
                    this.fileDropped = true;
                });
            } else {
                //     // It was a directory (empty directories are added, otherwise only files)
                //     const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
                //     console.log(droppedFile.relativePath, fileEntry);
            }
        }
        //this.onDropped.emit(filesToUpload);
    }

    public fileOver(event) {
        console.log(event);
    }

    public fileLeave(event) {
        console.log(event);
    }

    openFileUploadDialog() {
        let ref = this.dialog.open(FileUploadDialogComponent);
    }
}

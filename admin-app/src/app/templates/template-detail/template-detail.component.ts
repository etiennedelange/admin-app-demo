import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { IProductVersion, ITemplate, TemplateService } from '../template.service';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { UserRole } from 'src/app/shared/user-role';
import { MatDialog } from '@angular/material/dialog';
import { FileUploadDialogComponent } from '../file-upload-dialog/file-upload-dialog.component';
import { guidPattern } from 'src/app/common/constants';

@Component({
    selector: 'app-template-detail',
    templateUrl: './template-detail.component.html',
    styleUrls: ['./template-detail.component.less']
})
export class TemplateDetailComponent implements OnInit {
    constructor(
        private templateService: TemplateService,
        private snack: SnackBarService,
        private formBuilder: FormBuilder,
        private location: Location,
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService,
        private dialog: MatDialog) { }

    template: ITemplate;
    form: FormGroup;
    progressValue: number;
    droppedFile: File;

    allDesktopVersionNumbers: IProductVersion[];
    allOnlineVersionNumbers: IProductVersion[];

    get guidPattern(): RegExp {
        return guidPattern;
    }

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            description: ['', Validators.required],
            available: [false, Validators.required],
            guid: ['', [Validators.required]],
            hash: [{ value: '', disabled: true }],
            desktopProductVersions: [[], Validators.required],
            onlineProductVersions: [[], Validators.required]
        });

        this.route.data.subscribe((routeData: {
            template: ITemplate,
            allDesktopVersions: IProductVersion[],
            allOnlineVersions: IProductVersion[]
        }) => {
            this.template = routeData.template;
            this.allDesktopVersionNumbers = routeData.allDesktopVersions.sort(this.sortVersions);
            this.allOnlineVersionNumbers = routeData.allOnlineVersions.sort(this.sortVersions);

            if (this.template !== undefined) {
                this.form.setValue({
                    description: this.template.description,
                    available: this.template.available,
                    guid: this.template.guid,
                    desktopProductVersions: this.template.desktopProductVersions,
                    onlineProductVersions: this.template.onlineProductVersions,
                    hash: this.template.templateContentHash ?? ''
                });
            }
        });

        if (!this.userHasPermission()) {
            this.form.disable();
        }
    }

    compareVersions(a: IProductVersion, b: IProductVersion): boolean {
        return a && b ? a.id === b.id : a === b;
    }

    private sortVersions(a: IProductVersion, b: IProductVersion): number {
        return +a.versionNumber - +b.versionNumber;
    }

    public setDroppedFile(file: File) {
        this.droppedFile = file;
    }

    public isFormValid(): boolean {
        return this.template?.id > 0 ? this.form.valid : (this.form.valid && !!this.droppedFile);
    }

    public save(): void {
        let template: ITemplate = this.mapTemplateToSave();

        if (this.droppedFile) {
            this.saveWithFile(template);
        } else {
            this.templateService.save(template)
                .pipe(
                    catchError(err => {
                        this.snack.open({ message: err.error });
                        return throwError(err);
                    })
                )
                .subscribe(() => {
                    this.snack.open({ message: "Saved." });
                    this.form.markAsPristine();
                });
        }
    }

    private saveWithFile(template: ITemplate): void {
        this.templateService.saveWithTemplate(template, this.droppedFile)
            .pipe(
                catchError(err => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe((hash) => {
                this.snack.open({ message: "Saved." });
                this.form.markAsPristine();

                if (hash) {
                    this.form.patchValue({ hash: hash });
                    this.template.templateContentHash = hash;
                }
            });
    }

    public add(): void {
        let template = this.mapTemplateToSave();

        this.templateService
            .addTemplate(template, this.droppedFile)
            .pipe(
                catchError(err => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => {
                this.snack.open({ message: "Added." });
                this.form.markAsPristine();
                this.goBack();
            });
    }

    private mapTemplateToSave(): ITemplate {
        let template: ITemplate = { ...this.template, ...this.form.value };

        template.desktopProductVersions = [];
        template.onlineProductVersions = [];

        let selectedDesktopVersions: IProductVersion[] = this.form.get('desktopProductVersions').value;
        let selectedOnlineVersions: IProductVersion[] = this.form.get('onlineProductVersions').value;

        template.desktopProductVersions = selectedDesktopVersions;
        template.onlineProductVersions = selectedOnlineVersions;

        return template;
    }

    public goBack(): void {
        this.location.back()
    }

    public userHasPermission(): boolean {
        return this.authenticationService.hasRole(UserRole.Administrator) || this.authenticationService.hasRole(UserRole.ManageTemplates);
    }

    public openFileUploadDialog() {
        let dialogRef = this.dialog.open(FileUploadDialogComponent, {
            width: '550px'
        });
        dialogRef.afterClosed()
            .subscribe((file: File) => {
                if (file) {
                    this.droppedFile = file;
                    this.form.markAsDirty({ onlySelf: true });
                }
            });
    }
}

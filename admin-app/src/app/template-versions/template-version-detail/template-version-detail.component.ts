import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { UserRole } from 'src/app/shared/user-role';
import { IProductVersion } from 'src/app/templates/template.service';
import { TemplateDesktopVersionService, TemplateOnlineVersionService, TemplateVersionService } from '../template-version.services';

@Component({
    templateUrl: './template-version-detail.component.html'
})
export class TemplateVersionDetailComponent implements OnInit {

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private snack: SnackBarService,
        private location: Location,
        private authenticationService: AuthenticationService,
        private templateDesktopVersionService: TemplateDesktopVersionService,
        private templateOnlineVersionService: TemplateOnlineVersionService) {

    }

    templateVersion: IProductVersion;
    form: FormGroup;

    ngOnInit(): void {
        // Change to strongly typed form
        this.form = this.formBuilder.group({
            versionNumber: ['', [Validators.required]],
            releaseDate: [null, []],
        });

        this.route.data.subscribe((routeData: {
            templateVersion: IProductVersion
        }) => {
            this.templateVersion = routeData.templateVersion;

            if (this.templateVersion !== undefined) {
                this.form.setValue({
                    versionNumber: this.templateVersion.versionNumber,
                    releaseDate: this.templateVersion.releaseDate,
                });
            }
        });

        if (!this.userHasPermission()) {
            this.form.disable();
        }
    }

    private getService(): TemplateVersionService {
        if (this.route.routeConfig.path.startsWith('online')) {
            return this.templateOnlineVersionService
        } else return this.templateDesktopVersionService;

    }

    public userHasPermission(): boolean {
        return this.authenticationService.hasRole(UserRole.Administrator) || this.authenticationService.hasRole(UserRole.ManageTemplates);
    }

    public save(): void {
        // Merge original template version with form values using Object Spread (TS2.1)
        let templateVersion: IProductVersion = { ...this.templateVersion, ...this.form.value };

        let date = this.form.get('releaseDate').value as Date;
        templateVersion.releaseDate = date;

        this.getService()
            .save(templateVersion)
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

    public add(): void {
        // Merge original template version with form values using Object Spread (TS2.1)
        let templateVersion: IProductVersion = { ...this.templateVersion, ...this.form.value };

        this.getService()
            .add(templateVersion)
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

    public goBack(): void {
        this.location.back()
    }
}

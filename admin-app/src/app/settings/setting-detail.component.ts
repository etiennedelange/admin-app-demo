import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { SettingService, ISetting } from './setting.service';
import { Location } from '@angular/common';
import { AuthenticationService } from '../shared/services/authentication.service';
import { UserRole } from '../shared/user-role';
import { guidPattern } from '../common/constants';

@Component({
    selector: "settingDetail",
    templateUrl: "./setting-detail.component.html",
    styleUrls: ["./setting-detail.component.less"]
})
export class SettingDetailComponent implements OnInit {
    constructor(
        private formBuilder: FormBuilder,
        private settingService: SettingService,
        private route: ActivatedRoute,
        private location: Location,
        private snack: SnackBarService,
        private authService: AuthenticationService
    ) {
    }

    setting: ISetting;
    form: FormGroup;
    loading$ = of(true);
    title: string;

    get guidPattern(): RegExp {
        return guidPattern;
    }

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            description: ['', [Validators.required]],
            guid: ['', [Validators.required]],
            enabledGlobally: [false, [Validators.required]]
        });

        this.route.data.subscribe((routeData: {
            setting: ISetting
        }) => {
            this.setting = routeData.setting;
            // this.title = this.setting?.description ?? 'New Setting';

            this.form.setValue({
                description: this.setting.description,
                guid: this.setting.guid,
                enabledGlobally: this.setting.enabledGlobally ?? false
            });
        });

        this.loading$ = of(true);

        if (!this.userHasPermission()) {
            this.form.disable();
        }
    }

    public userHasPermission(): boolean {
        return this.authService.hasRole(UserRole.Administrator) || this.authService.hasRole(UserRole.ManageAttorneys);
    }

    public save(): void {
        // Merge original setting with form values using Object Spread (TS2.1)
        let setting: ISetting = { ...this.setting, ...this.form.value };

        this.settingService
            .save(setting)
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
        // Merge original attorney with form values using Object Spread (TS2.1)
        let setting: ISetting = { ...this.setting, ...this.form.value };

        this.settingService
            .add(setting)
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
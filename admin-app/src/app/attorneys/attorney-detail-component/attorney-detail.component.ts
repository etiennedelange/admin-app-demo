import { Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { IAttorney, AttorneyService } from '../attorney.service';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ISetting } from '../../settings/setting.service';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { Location } from '@angular/common';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { UserRole } from 'src/app/shared/user-role';
import { startWith } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { guidPattern } from 'src/app/common/constants';
import { MatTabChangeEvent } from '@angular/material/tabs';

@Component({
    selector: "attorneyDetail",
    templateUrl: "./attorney-detail.component.html",
    styleUrls: ["./attorney-detail.component.less"]
})

export class AttorneyDetailComponent implements OnInit {
    constructor(
        private formBuilder: FormBuilder,
        private attorneyService: AttorneyService,
        private route: ActivatedRoute,
        private location: Location,
        private snack: SnackBarService,
        private authenticationService: AuthenticationService,
        private dialogService: DialogService
    ) {
    }

    @ViewChild('filterInput') filterInput: ElementRef;

    @HostListener('document:keydown.control.s', ['$event']) onKeydownHandler(e: KeyboardEvent) {
        e.preventDefault();
        this.save();
    }

    attorney: IAttorney;
    allSettings: ISetting[];
    form: FormGroup;
    title: string;
    allChecked: boolean;
    filteredSettings: Observable<ISetting[]>;
    filterValue: string;

    get guidPattern(): RegExp {
        return guidPattern;
    }

    ngOnInit(): void {
        // Change to strongly typed form
        this.form = this.formBuilder.group({
            name: ['', [Validators.required]],
            kref: ['', [Validators.required]],
            branch: ['', [Validators.required]],
            lun: ['', [Validators.required]],
            altlun: ['', []],
            debtorCode: ['', []],
            activationDate: [null, []],
            onlineActivationChecked: [false, []],
            isHostedFirm: [false, []],
        });

        this.route.data.subscribe((routeData: {
            attorney: IAttorney,
            allSettings: ISetting[]
        }) => {
            this.attorney = routeData.attorney;
            this.allSettings = routeData.allSettings;

            if (this.attorney !== undefined) {
                this.attorney.settings?.forEach((activeSetting: ISetting) => {
                    var setting = this.allSettings.find((setting: ISetting) => setting.id == activeSetting.id)
                    if (setting) {
                        setting.enabled = true;
                    }
                });

                this.updateForm();
            }
        });

        if (!this.userHasPermission()) {
            this.form.disable();
        }

        this.updateAllChecked();
    }

    private updateForm() {
        this.form.setValue({
            name: this.attorney.name,
            kref: this.attorney.kref,
            branch: this.attorney.branch,
            lun: this.attorney.lun ?? null,
            altlun: this.attorney.altlun ?? null,
            debtorCode: this.attorney.debtorCode ?? null,
            activationDate: this.attorney.onlineActivationDate ?? null,
            onlineActivationChecked: this.attorney.onlineActivationChecked ?? null,
            isHostedFirm: this.attorney.isHostedFirm ?? null
        });
    }

    public attorneySelectionChanged(attorney: IAttorney): void {
        this.form.patchValue({
            name: attorney.name ?? '',
            kref: attorney.kref ?? '',
            branch: attorney.branch ?? '',
            debtorCode: attorney.debtorCode ?? ''
        });
    }

    public userHasPermission(): boolean {
        return this.authenticationService.hasRole(UserRole.Administrator) || this.authenticationService.hasRole(UserRole.ManageAttorneys);
    }

    private updateAllChecked() {
        this.allChecked = this.allSettings.every(x => x.enabled);
    }

    public someChecked(): boolean {
        return this.allSettings.some(x => x.enabled) && !this.allChecked;
    }

    public toggleAllSettings(e: MatCheckboxChange) {
        this.allSettings.forEach(x => x.enabled = e.checked);
        this.allChecked = true;
        this.form.markAsDirty();
    }

    public save(): void {
        // Merge original attorney with form values using Object Spread (TS2.1)
        let attorney: IAttorney = { ...this.attorney, ...this.form.value };

        let date = this.form.get('activationDate').value as Date;
        attorney.onlineActivationDate = date;

        this.setSelectedSettings(attorney);

        this.attorneyService
            .save(attorney)
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
        let attorney: IAttorney = { ...this.attorney, ...this.form.value };

        this.setSelectedSettings(attorney);

        this.attorneyService
            .add(attorney)
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

    private setSelectedSettings(attorney: IAttorney) {
        attorney.settings = this.allSettings.filter(x => x.enabled);
    }

    public goBack(): void {
        this.location.back()
    }

    public settingChanged(setting: ISetting): void {
        this.form.markAsDirty();
        this.updateAllChecked();

        if (!setting.enabled && setting.enabledGlobally) {
            this.snack.open({
                message: "This setting is enabled globally. Disabling it on this page has no effect.",
                durationMilliseconds: 8000
            });
        }
    }

    public filterSettings(): ISetting[] {
        return !this.filterValue
            ? this.allSettings
            : this.allSettings.filter(x => x.description.search(new RegExp(this.filterValue, 'i')) > -1);
    }

    public selectedTabChanged(event: MatTabChangeEvent) {
        if (event.tab.textLabel === 'Settings') {
            this.filterInput?.nativeElement?.focus();
        }
    }
}

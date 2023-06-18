import { Component, OnInit, Input, Inject } from '@angular/core';
import { IAttorney, AttorneyService, IAttorneyDialogData } from '../attorney.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { of } from 'rxjs';
import { catchError, finalize, tap, concatAll } from 'rxjs/operators';
import { SettingService, ISetting } from '../../settings/setting.service';

@Component({
    selector: "attorneyDetail",
    templateUrl: "./attorney-detail-dialog.component.html",
    styleUrls: ["./attorney-detail-dialog.component.less"]
})

export class AttorneyDetailDialogComponent implements OnInit {
    constructor(
        private dialogRef: MatDialogRef<AttorneyDetailDialogComponent>,
        private formBuilder: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: IAttorneyDialogData,
        private attorneyService: AttorneyService,
        private settingService: SettingService
    ) {
    }

    attorney: IAttorney;
    public settings: ISetting[];
    form: FormGroup;
    loading$ = of(true);
    title: string;

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            name: ['', [Validators.required], null, { updateOn: 'blur' }],
            kref: ['', [Validators.required], null, { updateOn: 'blur' }],
            branch: ['', [Validators.required], null, { updateOn: 'blur' }],
            lun: ['', [Validators.required], null, { updateOn: 'blur' }]
        });

        this.loading$ = of(true);
        let attorney$ = this.attorneyService
            .getAttorney(this.data.id)
            .pipe(
                tap(res => {
                    this.attorney = <IAttorney>res;
                    // this.title = this.attorney.name;
                    // Initialize form values
                    this.form.setValue({
                        name: this.attorney.name,
                        kref: this.attorney.kref,
                        branch: this.attorney.branch,
                        lun: this.attorney.lun
                    });
                }),
                catchError(() => of([]))
            );

        let settings$ = this.settingService
            .getAll()
            .pipe(
                tap((res) => this.settings = res)
            );

        let observables = of(attorney$, settings$);
        observables.pipe(
            concatAll(),
            finalize(() => this.loading$ = of(false)))
            .subscribe();
    }

    public save(): void {
        // Merge original attorney with form values using Object Spread (TS2.1)
        let attorney = { ...this.attorney, ...this.form.value };
        this.attorneyService.save(attorney)
            .subscribe(() => {
                this.dialogRef.close();
            });
    }
}
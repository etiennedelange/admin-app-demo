import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../shared/services/authentication.service';
import { SnackBarService } from '../shared/services/snack-bar.service';

@Component({
    selector: 'change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.less']
})
export class ChangePasswordComponent implements OnInit {
    form: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthenticationService,
        private snack: SnackBarService,
        private location: Location,
        private router: Router) { }

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            currentpassword: [''],
            newpassword: [''],
            confirmnewpassword: ['']
        });
    }

    public goBack() {
        this.location.back()
    }

    public enableButton(): boolean {
        return this.form.get('currentpassword')?.value && this.form.get('newpassword')?.value && this.form.get('confirmnewpassword')?.value;
    }

    public changePassword() {
        this.authService
            .changePassword(this.authService.getUsername(), this.form.get('currentpassword').value, this.form.get('newpassword').value)
            .pipe(
                first()
            )
            .subscribe({
                next: () => {
                    this.snack.open({ message: 'Password changed successfully' });
                    this.router.navigate(["/users"]);
                },
                error: (error) => this.snack.open({ message: error?.error })
            });
    }
}

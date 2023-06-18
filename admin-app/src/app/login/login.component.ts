import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from "../shared/services/authentication.service";
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from '../shared/services/snack-bar.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
    templateUrl: 'login.component.html',
    styleUrls: ["./login.component.less"]
})
export class LoginComponent implements OnInit {
    public error: any;

    constructor(
        private authService: AuthenticationService,
        private router: Router,
        private formBuilder: FormBuilder,
        private snackbarService: SnackBarService
    ) {
    }

    form: FormGroup;

    ngOnInit() {
        this.form = this.formBuilder.group({
            username: ['', [Validators.email, Validators.required], null, { updateOn: 'blur' }],
            password: ['', [Validators.required], null, { updateOn: 'blur' }]
        });
    }

    public login() {
        this.authService
            .login(this.form.get('username').value, this.form.get('password').value)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    this.snackbarService.open({ message: error.error });
                    return throwError(error);
                })
            )
            .subscribe(result => {
                if (result) {
                    this.router.navigate(["/home"]); // add redirect so that url that brought you here is navigated to after login
                }
            });
    }

    public navigateToForgetPassword() {
        let username = this.form.get('username').value;
        this.router.navigate(['/resetpassword', username]);
    }

}
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, IResetPassword } from '../shared/services/authentication.service';
import { SnackBarService } from '../shared/services/snack-bar.service';

@Component({
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.less']
})
export class ResetPasswordComponent implements OnInit {
    form: FormGroup;
    submitted: boolean = false;
    userId: string;
    token: string;
    emailConfirmed: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private authService: AuthenticationService,
        private formBuilder: FormBuilder,
        private snack: SnackBarService,
        private router: Router,
    ) {

    }

    ngOnInit(): void {
        this.userId = this.route.snapshot.queryParamMap.get('id');
        this.token = this.route.snapshot.queryParamMap.get('token');

        let username: string;
        if (this.routeActivatedFromEmail()) {
            this.route.data.subscribe((routeData: {
                resetPassword: IResetPassword
            }) => {
                username = routeData.resetPassword?.username;
                this.emailConfirmed = !!(routeData.resetPassword?.username);
            })
        } else {
            this.route.paramMap.subscribe(params => username = params?.get('username'));
        }

        if (!username) {
            this.snack.open({ message: `User with ID ${this.userId} not found` });
        }

        this.form = this.formBuilder.group({
            username: [username, [Validators.email, Validators.required]],
            password: ['', [Validators.required], null, { updateOn: 'blur' }],
            confirmPassword: ['', [Validators.required], null, { updateOn: 'blur' }]
        });
    }

    private routeActivatedFromEmail(): boolean {
        return !!(this.token && this.userId);
    }

    public reset(): void {
        if (this.routeActivatedFromEmail()) {
            let password = this.form.get('password').value;
            let confirmPassword = this.form.get('confirmPassword').value;

            // add proper client side password validation (uppercase, symbols, etc.)
            // add proper server side password validation (uppercase, symbols, etc.)
            if (password !== confirmPassword) {
                this.snack.open({ message: "Passwords do not match" });
                return;
            }

            this.authService.resetPassword({
                id: this.userId,
                token: this.token,
                newPassword: confirmPassword
            }).subscribe({
                next: () => {
                    this.snack.open({ message: "Password reset successfully. Please log in." });
                    this.router.navigate(["/login"]);
                },
                error: (error: HttpErrorResponse) => this.snack.open({ message: error.error }),
            });
        } else {
            this.authService
                .sendResetPasswordToken(this.form.get('username').value)
                .subscribe({
                    next: () => this.submitted = true,
                    error: (error: HttpErrorResponse) => this.snack.open({ message: error.error }),
                });
        }
    }
}
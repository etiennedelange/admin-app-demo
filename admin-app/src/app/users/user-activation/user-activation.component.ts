import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService, IResetPassword, IUser } from 'src/app/shared/services/authentication.service';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
    selector: 'user-activation',
    templateUrl: './user-activation.component.html',
    styleUrls: ['./user-activation.component.less']
})
export class UserActivationComponent implements OnInit {
    form: FormGroup;
    username: string;
    userId: string;
    token: string;
    emailConfirmed: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private userService: UserService,
        private authService: AuthenticationService,
        private router: Router,
        private formBuilder: FormBuilder,
        private snack: SnackBarService
    ) {
    }

    ngOnInit(): void {
        this.userId = this.route.snapshot.queryParamMap.get('id');
        this.token = this.route.snapshot.queryParamMap.get('token');

        this.route.data.subscribe((routeData: {
            resetPassword: IResetPassword
        }) => {
            this.username = routeData.resetPassword?.username;
        })

        if (!this.username) {
            this.snack.open({ message: `User with ID ${this.userId} not found` });
        } else {
            this.userService
                .activate(this.userId, this.token)
                .subscribe({
                    next: () => {
                        this.emailConfirmed = true;
                    },
                    error: (error: HttpErrorResponse) => {
                        this.snack.open({ message: error.error });
                    }
                });
        }

        this.form = this.formBuilder.group({
            username: [this.username ?? '', [Validators.email, Validators.required], null, { updateOn: 'blur' }],
            password: ['', [Validators.required], null, { updateOn: 'blur' }],
            confirmPassword: ['', [Validators.required], null, { updateOn: 'blur' }]
        });
    }

    errors: string[] = []
    register() {
        let password = this.form.get('password').value;
        let confirmPassword = this.form.get('confirmPassword').value;

        // add proper server side password validation (uppercase, symbols, etc.)
        if (password !== confirmPassword) {
            this.snack.open({ message: "Passwords do not match" });
            return;
        }

        this.authService
            .register(this.userId, this.token, password)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    this.errors = error.error;
                    return throwError(error);
                })
            )
            .subscribe(() => {
                this.snack.open({ message: "User registered. Please log in." });
                this.router.navigate(["/login"]);
            });
    }
}

export interface IUserActivation {
    id: number;
    url: string;
}
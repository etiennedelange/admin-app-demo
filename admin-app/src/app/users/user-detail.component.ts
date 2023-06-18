import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { IUser, UserService } from './user.service';
import { Location } from '@angular/common';
import { AuthenticationService } from '../shared/services/authentication.service';
import { UserRole } from '../shared/user-role';
import { emailPattern } from '../common/constants';

@Component({
    selector: "user-detail",
    templateUrl: "./user-detail.component.html",
    styleUrls: ["./user-detail.component.less"]
})
export class UserDetailComponent implements OnInit {
    constructor(
        private formBuilder: FormBuilder,
        private userService: UserService,
        private authenticationService: AuthenticationService,
        private route: ActivatedRoute,
        private location: Location,
        private snack: SnackBarService
    ) {
    }

    user: IUser;
    form: FormGroup;
    loading$ = of(true);
    title: string;

    get emailPattern(): RegExp {
        return emailPattern;
    }

    public allRoles: [string, boolean][] = [];

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            fullname: ['', Validators.required],
            username: ['', [Validators.required, Validators.email]],
            isactive: [false, Validators.required]
        });

        this.route.data.subscribe((routeData: {
            user: IUser
        }) => {
            this.user = routeData.user;

            this.form.setValue({
                fullname: this.user.fullName,
                username: this.user.userName,
                isactive: this.user.isActive
            });
        });

        this.loading$ = of(true);

        if (!this.userHasPermission()) {
            this.form.disable();
        }

        Object.values(UserRole).forEach((role) => {
            this.allRoles.push([role, false]);
        });

        this.allRoles.forEach((res) => {
            let role = res[0];
            if (this.user.roles?.findIndex(x => x === role) !== -1) {
                res[1] = true;
            }
        });
    }

    public userHasPermission(): boolean {
        return this.authenticationService.hasRole(UserRole.Administrator) || this.authenticationService.hasRole(UserRole.ManageUsers);
    }

    public roleChanged() {
        this.form.markAsDirty();
    }

    public save(): void {
        // Merge original user with form values using Object Spread (TS2.1)
        let user: IUser = { ...this.user, ...this.form.value };

        this.setUserRoles(user);

        this.userService
            .save(user)
            .pipe(
                catchError(err => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe({
                next: () => {
                    if (user.userName === this.authenticationService.getUsername()) {
                        this.snack.open({
                            message: "Saved. Please re-login for any permission changes to reflect.",
                            actionName: 'Logout',
                            action: () => this.authenticationService.logout(),
                            durationMilliseconds: 6000
                        })
                    } else {
                        this.snack.openMessage('Saved')
                    }
                    this.authenticationService.updateLocalUser({ roles: user.roles, username: user.userName });
                }
            });
    }

    public add(): void {
        // Merge original attorney with form values using Object Spread (TS2.1)
        let user: IUser = { ...this.user, ...this.form.value };

        this.setUserRoles(user);

        this.userService
            .add(user)
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

    private setUserRoles(user: IUser) {
        user.roles = [];
        this.allRoles.filter(x => x[1]).forEach((res) => {
            user.roles.push(res[0] as UserRole);
        });
    }

    public goBack(): void {
        this.location.back()
    }
}
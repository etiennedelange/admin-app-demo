import { Component, OnInit, Input, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { UserService, IUser, UsersDataSource } from './user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fromEvent, throwError } from 'rxjs';
import { distinctUntilChanged, tap, debounceTime, catchError } from 'rxjs/operators';
import { AuthenticationService } from '../shared/services/authentication.service';
import { UserRole } from '../shared/user-role';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from '../shared/services/snack-bar.service';

@Component({
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.less"]
})

export class UserListComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public total: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["Name", "Email", "Active", "Actions"];
    public dataSource: UsersDataSource;

    constructor(
        private userService: UserService,
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthenticationService,
        private snack: SnackBarService
    ) {
    }

    ngOnInit(): void {
        this.route.data.subscribe((routeData: {
            users: IUser[],
            total: number
        }) => {
            this.dataSource = new UsersDataSource(this.userService, routeData.users);
            this.total = routeData.total;
        });
    }

    // TODO: Extract table with paging to reusable component
    ngAfterViewInit(): void {
        this.paginator.page
            .pipe(
                tap(() => {
                    this.loadPage();
                })
            ).subscribe();

        this.sort.sortChange
            .pipe(
                tap(() => {
                    this.paginator.pageIndex = 0;
                })
            )
            .subscribe(() => this.loadPage());

        fromEvent(this.filter.nativeElement, 'keyup')
            .pipe(
                debounceTime(200),
                distinctUntilChanged(),
                tap(() => {
                    this.paginator.pageIndex = 0;
                    this.loadPage();
                })
            ).subscribe();
    }

    private loadPage() {
        this.dataSource.load(this.paginator.pageIndex, this.paginator.pageSize, this.filter.nativeElement.value, this.sort.direction);
    }

    public onUserClicked(row) {
        this.router.navigate(["/users", row.id]);
    }

    public addUser() {
        this.router.navigate(["/users/add"]);
    }

    public userHasPermissionToAdd() {
        return this.authService.hasRole(UserRole.Administrator) || this.authService.hasRole(UserRole.ManageUsers);
    }

    public delete(id: number) {
        this.userService.remove(id)
            .pipe(
                catchError((err: HttpErrorResponse) => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => this.loadPage());
    }
}
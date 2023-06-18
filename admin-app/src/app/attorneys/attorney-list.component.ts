import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { AttorneyService, IAttorney, AttorneysDataSource } from './attorney.service';
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
    templateUrl: "./attorney-list.component.html",
    styleUrls: ["./attorney-list.component.less"]
})

export class AttorneyListComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public total: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["Name", "Kref", "LUN", "DebtorCode", "Branch", "Actions"];
    public dataSource: AttorneysDataSource;

    constructor(
        private attorneyService: AttorneyService,
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthenticationService,
        private snack: SnackBarService
    ) {
    }

    ngOnInit(): void {
        this.route.data.subscribe((routeData: {
            attorneys: IAttorney[],
            total: number
        }) => {
            this.dataSource = new AttorneysDataSource(this.attorneyService, routeData.attorneys);
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

    public onAttorneyClicked(row) {
        this.router.navigate(["/attorneys", row.id]);
    }

    public addAttorney() {
        this.router.navigate(["/attorneys/add"]);
    }

    public userHasPermissionToAdd() {
        return this.authService.hasRole(UserRole.Administrator) || this.authService.hasRole(UserRole.ManageAttorneys);
    }

    public delete(id: number) {
        this.attorneyService.remove(id)
            .pipe(
                catchError((err: HttpErrorResponse) => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => this.loadPage());
    }
}
import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { tap, debounceTime, distinctUntilChanged, catchError } from "rxjs/operators";
import { ISetting, SettingsDataSource, SettingService } from "./setting.service";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { fromEvent, throwError } from "rxjs";
import { AuthenticationService } from '../shared/services/authentication.service';
import { UserRole } from '../shared/user-role';
import { HttpErrorResponse } from "@angular/common/http";
import { SnackBarService } from "../shared/services/snack-bar.service";

@Component({
    templateUrl: './setting-list.component.html'
})
export class SettingListComponent implements OnInit, AfterViewInit {
    constructor(
        private route: ActivatedRoute,
        private settingService: SettingService,
        private router: Router,
        private authService: AuthenticationService,
        private snack: SnackBarService
    ) {
    }

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public dataSource: SettingsDataSource;
    public total: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["Description", "Guid", "Enabled", "Actions"];

    ngOnInit(): void {
        this.route.data
            .subscribe((routeData: {
                settings: ISetting[],
                total: number
            }) => {
                this.dataSource = new SettingsDataSource(this.settingService, routeData.settings);
                this.total = routeData.total;
            });
    }

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

    loadPage() {
        this.dataSource.load(this.paginator.pageIndex, this.paginator.pageSize, this.filter.nativeElement.value, this.sort.direction);
    }

    onSettingClicked(row) {
        this.router.navigate(['/settings', row.id]);
    }

    addSetting() {
        this.router.navigate(["/settings/add"]);
    }

    public userHasPermissionToAdd() {
        return this.authService.hasRole(UserRole.Administrator) || this.authService.hasRole(UserRole.ManageAttorneys);
    }

    public delete(id: number) {
        this.settingService.remove(id)
            .pipe(
                catchError((err: HttpErrorResponse) => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => this.loadPage());
    }
}
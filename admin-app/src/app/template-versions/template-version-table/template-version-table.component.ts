import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter, Input, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fromEvent, throwError } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged, catchError } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { UserRole } from 'src/app/shared/user-role';
import { TemplateVersionsDataSource } from '../template-version.services';

@Component({
    selector: 'template-version-table',
    templateUrl: './template-version-table.component.html'
})
export class TemplateVersionTableComponent implements OnInit {
    constructor(
        private authService: AuthenticationService,
        private snack: SnackBarService
    ) {
    }

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    @Input() dataSource: TemplateVersionsDataSource;
    @Input() requiredAddRoles: UserRole[];
    @Input() paginatorLength: number;

    @Output() add: EventEmitter<any> = new EventEmitter();
    @Output() rowClicked: EventEmitter<any> = new EventEmitter();

    public pageSize: number = 10;
    public columnsToDisplay = ["VersionNumber", "ReleaseDate", "Actions"];

    ngOnInit(): void {
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

    public userHasPermissionToAdd() {
        return this.authService.hasSomeRoles(this.requiredAddRoles);
    }

    public delete(id: number) {
        this.dataSource.service.remove(id)
            .pipe(
                catchError((err: HttpErrorResponse) => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => this.loadPage());
    }
}

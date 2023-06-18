import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit, Pipe, PipeTransform, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { fromEvent, throwError } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged, catchError } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { UserRole } from 'src/app/shared/user-role';
import { ITemplate, TemplatesDataSource, TemplateService } from '../template.service';

@Component({
    selector: 'app-template-list',
    templateUrl: './template-list.component.html',
    styleUrls: ['./template-list.component.less']
})
export class TemplateListComponent implements OnInit, AfterViewInit {
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private templateService: TemplateService,
        private authService: AuthenticationService,
        private snack: SnackBarService
    ) { }

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public total: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["Description", "Guid", "Available", "Actions"];

    public dataSource: TemplatesDataSource;

    ngOnInit(): void {
        this.route.data
            .subscribe((routeData: {
                templates: ITemplate[],
                total: number
            }) => {
                this.dataSource = new TemplatesDataSource(this.templateService, routeData.templates);
                this.total = routeData.total;
            });
    }

    ngAfterViewInit(): void {
        this.paginator.page
            .pipe(
                tap(() => {
                    this.loadTemplatesPage();
                })
            ).subscribe();

        this.sort.sortChange
            .pipe(
                tap(() => {
                    this.paginator.pageIndex = 0;
                })
            )
            .subscribe(() => this.loadTemplatesPage());

        fromEvent(this.filter.nativeElement, 'keyup')
            .pipe(
                debounceTime(200),
                distinctUntilChanged(),
                tap(() => {
                    this.paginator.pageIndex = 0;
                    this.loadTemplatesPage();
                })
            ).subscribe();
    }

    loadTemplatesPage() {
        this.dataSource.load(this.paginator.pageIndex, this.paginator.pageSize, this.filter.nativeElement.value, this.sort.direction);
    }

    onRowClicked(row) {
        this.router.navigate(['/templatefiles', row.id]);
    }

    public addTemplate() {
        this.router.navigate(["/templatefiles/add"]);
    }

    public deleteTemplate(id: number) {
        this.templateService.remove(id)
            .pipe(
                catchError((err: HttpErrorResponse) => {
                    this.snack.open({ message: err.error });
                    return throwError(err);
                })
            )
            .subscribe(() => this.loadTemplatesPage());
    }

    public userHasPermissionToAdd() {
        return this.authService.hasRole(UserRole.Administrator) || this.authService.hasRole(UserRole.ManageTemplates);
    }
}
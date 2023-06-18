import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { AuditLogDataSource, AuditLogService, IAuditLog } from './audit-log.service';

@Component({
    templateUrl: './audit-log-list.component.html',
    styleUrls: ['./audit-log-list.component.less']
})
export class AuditLogListComponent implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private auditLogService: AuditLogService
    ) {
    }

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public dataSource: AuditLogDataSource;
    public total: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["User", "Date", "Data"];

    ngOnInit(): void {
        this.route.data
            .subscribe((routeData: {
                auditLogs: IAuditLog[],
                total: number
            }) => {
                this.dataSource = new AuditLogDataSource(this.auditLogService, routeData.auditLogs);
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

    onRowClicked(row) {
    }
}

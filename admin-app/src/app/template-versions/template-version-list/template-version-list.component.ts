import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { UserRole } from 'src/app/shared/user-role';
import { IProductVersion } from 'src/app/templates/template.service';
import { TemplateDesktopVersionService, TemplateOnlineVersionService, TemplateVersionsDataSource } from '../template-version.services';

@Component({
    templateUrl: './template-version-list.component.html'
})
export class TemplateVersionListComponent implements OnInit {
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private templateDesktopVersionService: TemplateDesktopVersionService,
        private templateOnlineVersionService: TemplateOnlineVersionService) { }

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;
    @ViewChild('filter', { static: false }) filter: ElementRef;

    public desktopVersionsDataSource: TemplateVersionsDataSource;
    public onlineVersionsDataSource: TemplateVersionsDataSource;
    public desktopTemplateVersionTotal: number;
    public onlineTemplateVersionTotal: number;
    public pageSize: number = 10;
    public columnsToDisplay = ["VersionNumber", "ReleaseDate"];

    ngOnInit(): void {
        this.route.data
            .subscribe((routeData: {
                desktopTemplatesVersions: IProductVersion[],
                onlineTemplatesVersions: IProductVersion[],
                desktopTemplateVersionTotal: number,
                onlineTemplateVersionTotal: number,
            }) => {
                this.desktopVersionsDataSource = new TemplateVersionsDataSource(this.templateDesktopVersionService, routeData.desktopTemplatesVersions);
                this.onlineVersionsDataSource = new TemplateVersionsDataSource(this.templateOnlineVersionService, routeData.onlineTemplatesVersions);
                this.desktopTemplateVersionTotal = routeData.desktopTemplateVersionTotal;
                this.onlineTemplateVersionTotal = routeData.onlineTemplateVersionTotal;
            });
    }

    onDesktopVersionClicked(row: any) {
        this.router.navigate(['/templateversions/desktop', row.id]);
    }

    onOnlineVersionClicked(row: any) {
        this.router.navigate(['/templateversions/online', row.id]);
    }

    addDesktopTemplateVersion() {
        this.router.navigate(["/templateversions/desktop/add"]);
    }

    addOnlineTemplateVersion() {
        this.router.navigate(["/templateversions/online/add"]);
    }

    public getRequiredRolesToAdd(): UserRole[] {
        return [UserRole.ManageTemplates, UserRole.Administrator];
    }
}

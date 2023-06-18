import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportsComponent } from './reports.component';
import { ReportsRoutingModule } from './reports.routing.module';
import { ReportListComponent } from './report-list/report-list.component';
import { AppSharedModule } from '../app.shared.module';

@NgModule({
	declarations: [
		ReportsComponent,
		ReportListComponent
	],
	imports: [
		CommonModule,
		ReportsRoutingModule,
		AppSharedModule
	],
})
export class ReportsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReportsComponent } from './reports.component';
import { ReportListComponent } from './report-list/report-list.component';
import { AuthGuard } from '../auth-guard.service';

export const routes: Routes = [
	{
		path: '',
		canActivate: [AuthGuard],
		component: ReportsComponent,
		children: [
			{
				path: '',
				component: ReportListComponent
			}
		]
	}
]

@NgModule({
	declarations: [],
	imports: [
		RouterModule.forChild(routes)
	],
	exports: [RouterModule]
})
export class ReportsRoutingModule {

}

import { NgModule } from "@angular/core";
import { AttorneysRoutingModule } from "./attorneys.routing.module";
import { AttorneysComponent } from "./attorneys.component";
import { AttorneyListComponent } from "./attorney-list.component";
import { AppSharedModule } from "../app.shared.module";
import { AttorneyDetailComponent } from './attorney-detail-component/attorney-detail.component';

@NgModule({
	imports: [
		AttorneysRoutingModule,
		AppSharedModule
	],
	declarations: [
		AttorneysComponent,
		AttorneyListComponent,
		AttorneyDetailComponent
	],
	entryComponents: [AttorneyDetailComponent]
})
export class AttorneysModule { }
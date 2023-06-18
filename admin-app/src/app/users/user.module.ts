import { NgModule } from "@angular/core";
import { UsersRoutingModule } from "./user.routing.module";
import { AppSharedModule } from "../app.shared.module";
import { UsersComponent } from './user.component';
import { UserListComponent } from './user-list.component';
import { UserDetailComponent } from './user-detail.component';
import { UserActivationComponent } from './user-activation/user-activation.component';

@NgModule({
    imports: [
        UsersRoutingModule,
        AppSharedModule
    ],
    declarations: [
        UsersComponent,
        UserListComponent,
        UserDetailComponent
    ],
    entryComponents: [UserDetailComponent]
})
export class UsersModule { }
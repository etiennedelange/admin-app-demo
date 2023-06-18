import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../auth-guard.service";
import { UserListResolver, UserCountResolver, UserDetailResolverService } from './user-list-resolver.service';
import { UsersComponent } from './user.component';
import { UserListComponent } from './user-list.component';
import { UserDetailComponent } from './user-detail.component';

export const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        component: UsersComponent,
        children: [
            {

                path: '',
                component: UserListComponent,
                resolve: {
                    users: UserListResolver,
                    total: UserCountResolver
                }
            },
            {
                path: 'add',
                component: UserDetailComponent
            },
            {
                path: ':id',
                component: UserDetailComponent,
                resolve: {
                    user: UserDetailResolverService
                }
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})

export class UsersRoutingModule {

}
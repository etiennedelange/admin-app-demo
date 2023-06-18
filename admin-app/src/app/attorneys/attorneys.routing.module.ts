import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AttorneysComponent } from "./attorneys.component";
import { AttorneyListComponent } from "./attorney-list.component";
import { AttorneyDetailComponent } from "./attorney-detail-component/attorney-detail.component";
import { AuthGuard } from "../auth-guard.service";
import { AttorneyResolver, AttorneyCountResolver, AttorneyDetailResolverService } from "./attorneys-list-resolver.service";
import { SettingsResolverService } from '../settings/setting-list-resolver.service';

export const routes: Routes = [
    {
        path: '',
        component: AttorneysComponent,
        data: { animation: 'attorneys' },
        canActivate: [AuthGuard],
        children: [
            {
                path: '',
                component: AttorneyListComponent,
                resolve: {
                    attorneys: AttorneyResolver,
                    total: AttorneyCountResolver
                }
            },
            {
                path: 'add',
                component: AttorneyDetailComponent,
                resolve: {
                    allSettings: SettingsResolverService
                }
            },
            {
                path: ':id',
                component: AttorneyDetailComponent,
                resolve: {
                    attorney: AttorneyDetailResolverService,
                    allSettings: SettingsResolverService
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

export class AttorneysRoutingModule {

}
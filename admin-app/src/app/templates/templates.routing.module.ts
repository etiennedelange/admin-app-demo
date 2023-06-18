import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth-guard.service';
import { TemplatesComponent } from './templates.component';
import { TemplateListComponent } from './template-list/template-list.component';
import { TemplateDetailComponent } from './template-detail/template-detail.component';
import { TemplateDetailResolverService } from './template-detail/template-detail-resolver.service';
import { TemplatePageResolverService } from './template-list/template-page-resolver.service';
import { TemplateCountResolverService } from './template-list/template-count-resolver.service';
import { TemplateDesktopVersionListResolverService, TemplateOnlineVersionListResolverService } from '../template-versions/template-version-list/template-version-list-resolver.services';

export const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        component: TemplatesComponent,
        children: [
            {
                path: '',
                component: TemplateListComponent,
                resolve: {
                    templates: TemplatePageResolverService,
                    total: TemplateCountResolverService
                }
            },
            {
                path: 'add',
                component: TemplateDetailComponent,
                resolve: {
                    allDesktopVersions: TemplateDesktopVersionListResolverService,
                    allOnlineVersions: TemplateOnlineVersionListResolverService
                }
            },
            {
                path: ':id',
                component: TemplateDetailComponent,
                resolve: {
                    template: TemplateDetailResolverService,
                    allDesktopVersions: TemplateDesktopVersionListResolverService,
                    allOnlineVersions: TemplateOnlineVersionListResolverService
                }
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
export class TemplatesRoutingModule { }

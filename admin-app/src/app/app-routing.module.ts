import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard, NotAuthGuard } from "./auth-guard.service";
import { LocationStrategy, PathLocationStrategy, APP_BASE_HREF } from '@angular/common';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UserActivationComponent } from './users/user-activation/user-activation.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResetPasswordResolver } from './users/user-list-resolver.service';
import { HomeComponent } from './home/home.component';
import { DevelopmentFeatureGuard } from './development-feature-guard.service';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'attorneys',
        pathMatch: 'full'
    },
    {
        path: 'login',
        canActivate: [NotAuthGuard],
        component: LoginComponent,
    },
    {
        path: 'home',
        canActivate: [AuthGuard],
        component: HomeComponent,
    },
    {
        path: 'activateuser',
        component: UserActivationComponent, // check this and move out of users module/folder
        resolve: {
            resetPassword: ResetPasswordResolver
        }
    },
    {
        path: 'resetpassword/:username',
        canActivate: [NotAuthGuard],
        component: ResetPasswordComponent
    },
    {
        path: 'resetpassword',
        canActivate: [NotAuthGuard],
        component: ResetPasswordComponent,
        resolve: {
            resetPassword: ResetPasswordResolver
        }
    },
    {
        path: 'attorneys',
        canActivate: [AuthGuard],
        loadChildren: () => import('./attorneys/attorneys.module').then(a => a.AttorneysModule)
    },
    {
        path: 'users',
        canActivate: [AuthGuard],
        loadChildren: () => import('./users/user.module').then(a => a.UsersModule)
    },
    {
        path: 'settings',
        canActivate: [AuthGuard],
        loadChildren: () => import('./settings/settings.module').then(a => a.SettingsModule)
    },
    {
        path: 'reports',
        canActivate: [AuthGuard],
        loadChildren: () => import('./reports/reports.module').then(a => a.ReportsModule)
    },
    {
        path: 'templatefiles',
        canActivate: [AuthGuard],
        loadChildren: () => import('./templates/templates.module').then(a => a.TemplatesModule)
    },
    {
        path: 'templateversions',
        canActivate: [AuthGuard],
        loadChildren: () => import('./template-versions/template-versions.module').then(a => a.TemplateVersionsModule)
    },
    {
        path: 'changepassword',
        canActivate: [AuthGuard],
        component: ChangePasswordComponent
    },
    {
        path: 'auditlogs',
        canActivate: [AuthGuard],
        loadChildren: () => import('./audit-log/audit-log.module').then(a => a.AuditLogModule)
    },
    {
        path: 'sandbox',
        canActivate: [DevelopmentFeatureGuard],
        loadChildren: () => import('./sandbox/sandbox.module').then(a => a.SandboxModule)
    },
    {
        path: '**',
        component: PageNotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(
        routes,
        {
            //enableTracing: true,
            // useHash: true,
            preloadingStrategy: PreloadAllModules,
            relativeLinkResolution: 'legacy'
        }
    )],
    exports: [RouterModule],
    providers: [
        AuthGuard,
        { provide: LocationStrategy, useClass: PathLocationStrategy },
        // { provide: APP_BASE_HREF, useValue: '/' }
    ]
})
export class AppRoutingModule { }
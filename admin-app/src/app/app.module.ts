import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LoginComponent } from './login/login.component';
import { AppHeaderComponent } from './header/app-header.component';
import { AppSharedModule } from "./app.shared.module";
import { AppNavMenuComponent } from './nav-menu/app-nav-menu.component';
import { LoadingBarComponent } from './common/app-loading-bar/loading-bar.component'
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ChangePasswordComponent } from './change-password/change-password.component'
import { UserActivationComponent } from './users/user-activation/user-activation.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { Router } from "@angular/router";
import { ConfirmationDialogComponent } from './common/confirmation-dialog/confirmation-dialog.component';
import { AppSidenavComponent } from "./common/app-sidenav/app-sidenav.component";
import { HomeComponent } from "./home/home.component";
import { LottieModule } from 'ngx-lottie';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        FormsModule,
        BrowserAnimationsModule,
        AppSharedModule,
        LottieModule.forRoot({
            player: playerFactory,
            useCache: true
        }),
        ToastrModule.forRoot({
            easing: 'linear',
            maxOpened: 18,
            timeOut: 6000
        })
    ],
    declarations: [
        AppComponent,
        AppNavMenuComponent,
        LoginComponent,
        UserActivationComponent,
        AppHeaderComponent,
        LoadingBarComponent,
        PageNotFoundComponent,
        ChangePasswordComponent,
        ResetPasswordComponent,
        ConfirmationDialogComponent,
        AppSidenavComponent,
        HomeComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
    constructor(router: Router) {
    }
}

export function playerFactory() {
    return import(/* webpackChunkName: 'lottie-web' */ 'lottie-web/build/player/lottie_svg');
}
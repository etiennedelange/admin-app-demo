﻿import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../auth-guard.service";
import { SandboxComponent } from "./sandbox.component";

export const routes: Routes = [
    {
        path: '',
        component: SandboxComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})

export class SandboxRoutingModule {

}
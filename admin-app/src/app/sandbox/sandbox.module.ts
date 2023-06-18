import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SandboxComponent } from './sandbox.component';
import { AppSharedModule } from '../app.shared.module';
import { SandboxRoutingModule } from './sandbox.routing.module';



@NgModule({
    declarations: [
        SandboxComponent
    ],
    imports: [
        AppSharedModule,
        SandboxRoutingModule,
        CommonModule
    ]
})
export class SandboxModule { }

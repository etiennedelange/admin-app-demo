import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { LottieModule } from 'ngx-lottie';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppMaterialModule } from 'src/app/app.material.module';
import { playerFactory } from 'src/app/app.module';
import { AppSharedModule } from 'src/app/app.shared.module';
import { TemplateVersionListComponent } from './template-version-list.component';

describe('TemplateVersionListComponent', () => {
    let component: TemplateVersionListComponent;
    let fixture: ComponentFixture<TemplateVersionListComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                RouterTestingModule,
                HttpClientTestingModule,
                BrowserModule,
                HttpClientModule,
                AppRoutingModule,
                FormsModule,
                BrowserAnimationsModule,
                AppSharedModule,
                LottieModule.forRoot({
                    player: playerFactory,
                    useCache: true
                })
            ],
            declarations: [TemplateVersionListComponent]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TemplateVersionListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

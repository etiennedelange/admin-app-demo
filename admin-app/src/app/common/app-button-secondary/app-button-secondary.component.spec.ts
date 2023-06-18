import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppButtonSecondaryComponent } from './app-button-secondary.component';

describe('AppButtonSecondaryComponent', () => {
    let component: AppButtonSecondaryComponent;
    let fixture: ComponentFixture<AppButtonSecondaryComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [
                AppButtonSecondaryComponent
            ]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppButtonSecondaryComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

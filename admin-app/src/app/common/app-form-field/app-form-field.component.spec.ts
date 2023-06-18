import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NgControl } from '@angular/forms';
import { NgControlMock } from 'src/tests/mocks';

import { AppFormFieldComponent } from './app-form-field.component';

describe('AppFormFieldComponent', () => {
    let component: AppFormFieldComponent;
    let fixture: ComponentFixture<AppFormFieldComponent>;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [],
            declarations: [
                AppFormFieldComponent
            ],
            providers: [
                { provide: NgControl, useClass: NgControlMock }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AppFormFieldComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

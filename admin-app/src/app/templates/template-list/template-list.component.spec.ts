import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { SnackBarServiceMock } from 'src/tests/mocks';

import { TemplateListComponent } from './template-list.component';

describe('TemplateListComponent', () => {
    let component: TemplateListComponent;
    let fixture: ComponentFixture<TemplateListComponent>;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                RouterTestingModule,
                MatPaginatorModule,
                BrowserAnimationsModule,
                MatSortModule
            ],
            declarations: [TemplateListComponent],
            providers: [
                { provide: MatSnackBar, useClass: SnackBarServiceMock }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TemplateListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

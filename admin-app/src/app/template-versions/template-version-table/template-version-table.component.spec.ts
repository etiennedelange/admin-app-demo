import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { AuthenticationServiceMock, SnackBarServiceMock } from 'src/tests/mocks';
import { TemplateVersionTableComponent } from './template-version-table.component';

describe('TemplateVersionTableComponent', () => {
    let component: TemplateVersionTableComponent;
    let fixture: ComponentFixture<TemplateVersionTableComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                RouterTestingModule,
                MatPaginatorModule,
                BrowserAnimationsModule,
                MatSortModule
            ],
            declarations: [
                TemplateVersionTableComponent],
            providers: [
                { provide: MatSnackBar, useClass: SnackBarServiceMock },
                { provide: AuthenticationService, useClass: AuthenticationServiceMock }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TemplateVersionTableComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

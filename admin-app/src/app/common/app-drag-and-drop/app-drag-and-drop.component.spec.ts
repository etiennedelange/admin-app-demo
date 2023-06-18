import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialog } from '@angular/material/dialog';
import { RouterTestingModule } from '@angular/router/testing';

import { DragAndDropComponent } from './app-drag-and-drop.component';

describe('DragAndDropComponent', () => {
    let component: DragAndDropComponent;
    let fixture: ComponentFixture<DragAndDropComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            declarations: [DragAndDropComponent],
            providers: [
                { provide: MatDialog, useValue: {} }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(DragAndDropComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

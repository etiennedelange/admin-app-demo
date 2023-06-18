import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppButtonPrimaryComponent } from './app-button-primary.component';

describe('AppButtonPrimaryComponent', () => {
  let component: AppButtonPrimaryComponent;
  let fixture: ComponentFixture<AppButtonPrimaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppButtonPrimaryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppButtonPrimaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

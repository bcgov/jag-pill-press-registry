import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportingSalesFormComponent } from './reporting-sales-form.component';

describe('ReportingSalesFormComponent', () => {
  let component: ReportingSalesFormComponent;
  let fixture: ComponentFixture<ReportingSalesFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportingSalesFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportingSalesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

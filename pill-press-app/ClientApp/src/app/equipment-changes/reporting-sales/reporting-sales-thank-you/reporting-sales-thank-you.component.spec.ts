import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportingSalesThankYouComponent } from './reporting-sales-thank-you.component';

describe('ReportingSalesThankYouComponent', () => {
  let component: ReportingSalesThankYouComponent;
  let fixture: ComponentFixture<ReportingSalesThankYouComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportingSalesThankYouComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportingSalesThankYouComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportingSalesReviewComponent } from './reporting-sales-review.component';

describe('ReportingSalesReviewComponent', () => {
  let component: ReportingSalesReviewComponent;
  let fixture: ComponentFixture<ReportingSalesReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportingSalesReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportingSalesReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerApplicationReviewComponent } from './seller-application-review.component';

describe('SellerApplicationReviewComponent', () => {
  let component: SellerApplicationReviewComponent;
  let fixture: ComponentFixture<SellerApplicationReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerApplicationReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerApplicationReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

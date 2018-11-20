import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WaiverReviewComponent } from './waiver-review.component';

describe('WaiverReviewComponent', () => {
  let component: WaiverReviewComponent;
  let fixture: ComponentFixture<WaiverReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WaiverReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WaiverReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

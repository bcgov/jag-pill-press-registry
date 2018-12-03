import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedApplicationReviewComponent } from './authorized-application-review.component';

describe('AuthorizedApplicationReviewComponent', () => {
  let component: AuthorizedApplicationReviewComponent;
  let fixture: ComponentFixture<AuthorizedApplicationReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorizedApplicationReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedApplicationReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

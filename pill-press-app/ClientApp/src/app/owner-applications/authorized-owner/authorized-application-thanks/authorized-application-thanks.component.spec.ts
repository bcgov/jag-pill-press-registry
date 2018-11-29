import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedApplicationThanksComponent } from './authorized-application-thanks.component';

describe('AuthorizedApplicationThanksComponent', () => {
  let component: AuthorizedApplicationThanksComponent;
  let fixture: ComponentFixture<AuthorizedApplicationThanksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorizedApplicationThanksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedApplicationThanksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

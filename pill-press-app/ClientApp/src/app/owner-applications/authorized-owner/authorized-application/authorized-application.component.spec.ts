import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedApplicationComponent } from './authorized-application.component';

describe('AuthorizedApplicationComponent', () => {
  let component: AuthorizedApplicationComponent;
  let fixture: ComponentFixture<AuthorizedApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorizedApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

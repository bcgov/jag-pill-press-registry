import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedOwnerComponent } from './authorized-owner.component';

describe('AuthorizedOwnerComponent', () => {
  let component: AuthorizedOwnerComponent;
  let fixture: ComponentFixture<AuthorizedOwnerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorizedOwnerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerApplicationThanksComponent } from './seller-application-thanks.component';

describe('SellerApplicationThanksComponent', () => {
  let component: SellerApplicationThanksComponent;
  let fixture: ComponentFixture<SellerApplicationThanksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerApplicationThanksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerApplicationThanksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

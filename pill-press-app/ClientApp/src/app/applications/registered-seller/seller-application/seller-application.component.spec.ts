import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerApplicationComponent } from './seller-application.component';

describe('SellerApplicationComponent', () => {
  let component: SellerApplicationComponent;
  let fixture: ComponentFixture<SellerApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

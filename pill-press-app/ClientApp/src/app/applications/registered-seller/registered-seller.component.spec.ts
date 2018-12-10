import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisteredSellerComponent } from './registered-seller.component';

describe('RegisteredSellerComponent', () => {
  let component: RegisteredSellerComponent;
  let fixture: ComponentFixture<RegisteredSellerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisteredSellerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisteredSellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

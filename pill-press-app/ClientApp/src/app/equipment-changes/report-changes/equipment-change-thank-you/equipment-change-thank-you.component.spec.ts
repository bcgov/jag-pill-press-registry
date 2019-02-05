import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentChangeThankYouComponent } from './equipment-change-thank-you.component';

describe('EquipmentChangeThankYouComponent', () => {
  let component: EquipmentChangeThankYouComponent;
  let fixture: ComponentFixture<EquipmentChangeThankYouComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentChangeThankYouComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentChangeThankYouComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

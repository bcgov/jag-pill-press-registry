import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentThankYouComponent } from './equipment-thank-you.component';

describe('EquipmentThankYouComponent', () => {
  let component: EquipmentThankYouComponent;
  let fixture: ComponentFixture<EquipmentThankYouComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentThankYouComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentThankYouComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

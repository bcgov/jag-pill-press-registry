import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentNotificationComponent } from './equipment-notification.component';

describe('EquipmentNotificationComponent', () => {
  let component: EquipmentNotificationComponent;
  let fixture: ComponentFixture<EquipmentNotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentNotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

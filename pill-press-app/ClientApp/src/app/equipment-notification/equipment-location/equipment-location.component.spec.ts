import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentLocationComponent } from './equipment-location.component';

describe('EquipmentLocationComponent', () => {
  let component: EquipmentLocationComponent;
  let fixture: ComponentFixture<EquipmentLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

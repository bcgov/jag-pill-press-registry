import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentIdentificationComponent } from './equipment-identification.component';

describe('EquipmentIdentificationComponent', () => {
  let component: EquipmentIdentificationComponent;
  let fixture: ComponentFixture<EquipmentIdentificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentIdentificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentIdentificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

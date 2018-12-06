import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentSourceComponent } from './equipment-source.component';

describe('EquipmentSourceComponent', () => {
  let component: EquipmentSourceComponent;
  let fixture: ComponentFixture<EquipmentSourceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentSourceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentSourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

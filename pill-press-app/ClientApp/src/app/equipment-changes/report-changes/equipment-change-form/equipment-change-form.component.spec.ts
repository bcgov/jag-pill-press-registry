import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentChangeFormComponent } from './equipment-change-form.component';

describe('EquipmentChangeFormComponent', () => {
  let component: EquipmentChangeFormComponent;
  let fixture: ComponentFixture<EquipmentChangeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentChangeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentChangeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentTypeAndUseComponent } from './equipment-type-and-use.component';

describe('EquipmentTypeAndUseComponent', () => {
  let component: EquipmentTypeAndUseComponent;
  let fixture: ComponentFixture<EquipmentTypeAndUseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentTypeAndUseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentTypeAndUseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

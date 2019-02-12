import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentChangeReviewComponent } from './equipment-change-review.component';

describe('EquipmentChangeReviewComponent', () => {
  let component: EquipmentChangeReviewComponent;
  let fixture: ComponentFixture<EquipmentChangeReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentChangeReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentChangeReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

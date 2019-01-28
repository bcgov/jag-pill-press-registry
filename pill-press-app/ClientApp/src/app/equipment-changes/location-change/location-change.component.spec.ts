import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationChangeComponent } from './location-change.component';

describe('LocationChangeComponent', () => {
  let component: LocationChangeComponent;
  let fixture: ComponentFixture<LocationChangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LocationChangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

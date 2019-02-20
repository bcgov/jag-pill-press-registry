import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportingSalesComponent } from './reporting-sales.component';

describe('ReportingSalesComponent', () => {
  let component: ReportingSalesComponent;
  let fixture: ComponentFixture<ReportingSalesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportingSalesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportingSalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

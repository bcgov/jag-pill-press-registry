import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportChangesComponent } from './report-changes.component';

describe('ReportChangesComponent', () => {
  let component: ReportChangesComponent;
  let fixture: ComponentFixture<ReportChangesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportChangesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportChangesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

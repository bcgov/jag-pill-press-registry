import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VersionInfoDialog } from './version-info.component';

describe('VersionInfoDialog', () => {
  let component: VersionInfoDialog;
  let fixture: ComponentFixture<VersionInfoDialog>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VersionInfoDialog ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VersionInfoDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

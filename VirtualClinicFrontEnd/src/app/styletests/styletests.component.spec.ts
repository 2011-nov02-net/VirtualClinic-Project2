import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StyletestsComponent } from './styletests.component';

describe('StyletestsComponent', () => {
  let component: StyletestsComponent;
  let fixture: ComponentFixture<StyletestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StyletestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StyletestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

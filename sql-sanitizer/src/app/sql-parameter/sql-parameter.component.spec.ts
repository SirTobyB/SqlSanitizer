import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SqlParameterComponent } from './sql-parameter.component';

describe('SqlParameterComponent', () => {
  let component: SqlParameterComponent;
  let fixture: ComponentFixture<SqlParameterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SqlParameterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SqlParameterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

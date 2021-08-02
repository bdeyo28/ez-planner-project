import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddattendeeComponent } from './addattendee.component';

describe('AddattendeeComponent', () => {
  let component: AddattendeeComponent;
  let fixture: ComponentFixture<AddattendeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddattendeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddattendeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditattendeeComponent } from './editattendee.component';

describe('EditattendeeComponent', () => {
  let component: EditattendeeComponent;
  let fixture: ComponentFixture<EditattendeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditattendeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditattendeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

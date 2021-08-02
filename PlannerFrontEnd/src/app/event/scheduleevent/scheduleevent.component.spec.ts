import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleeventComponent } from './scheduleevent.component';

describe('ScheduleeventComponent', () => {
  let component: ScheduleeventComponent;
  let fixture: ComponentFixture<ScheduleeventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScheduleeventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleeventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

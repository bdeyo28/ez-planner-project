import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FindeventComponent } from './findevent.component';

describe('FindeventComponent', () => {
  let component: FindeventComponent;
  let fixture: ComponentFixture<FindeventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FindeventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FindeventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

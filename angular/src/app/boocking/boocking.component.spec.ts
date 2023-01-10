import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoockingComponent } from './boocking.component';

describe('BoockingComponent', () => {
  let component: BoockingComponent;
  let fixture: ComponentFixture<BoockingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BoockingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoockingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

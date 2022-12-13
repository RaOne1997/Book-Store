import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassbackComponent } from './classback.component';

describe('ClassbackComponent', () => {
  let component: ClassbackComponent;
  let fixture: ComponentFixture<ClassbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassbackComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

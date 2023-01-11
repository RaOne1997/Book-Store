import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConformpasswordComponent } from './conformpassword.component';

describe('ConformpasswordComponent', () => {
  let component: ConformpasswordComponent;
  let fixture: ComponentFixture<ConformpasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConformpasswordComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConformpasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

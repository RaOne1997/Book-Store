import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarwithsideComponent } from './navbarwithside.component';

describe('NavbarwithsideComponent', () => {
  let component: NavbarwithsideComponent;
  let fixture: ComponentFixture<NavbarwithsideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarwithsideComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarwithsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseAppliesComponent } from './course-applies.component';

describe('CourseAppliesComponent', () => {
  let component: CourseAppliesComponent;
  let fixture: ComponentFixture<CourseAppliesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseAppliesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseAppliesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

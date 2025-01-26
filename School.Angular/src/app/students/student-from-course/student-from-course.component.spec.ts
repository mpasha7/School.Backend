import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentFromCourseComponent } from './student-from-course.component';

describe('StudentFromCourseComponent', () => {
  let component: StudentFromCourseComponent;
  let fixture: ComponentFixture<StudentFromCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudentFromCourseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentFromCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

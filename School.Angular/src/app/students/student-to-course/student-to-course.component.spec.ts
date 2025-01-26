import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentToCourseComponent } from './student-to-course.component';

describe('StudentToCourseComponent', () => {
  let component: StudentToCourseComponent;
  let fixture: ComponentFixture<StudentToCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudentToCourseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentToCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

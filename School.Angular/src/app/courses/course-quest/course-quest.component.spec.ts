import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseQuestComponent } from './course-quest.component';

describe('CourseQuestComponent', () => {
  let component: CourseQuestComponent;
  let fixture: ComponentFixture<CourseQuestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseQuestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseQuestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

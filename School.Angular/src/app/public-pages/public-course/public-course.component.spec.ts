import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicCourseComponent } from './public-course.component';

describe('PublicCourseComponent', () => {
  let component: PublicCourseComponent;
  let fixture: ComponentFixture<PublicCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PublicCourseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PublicCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

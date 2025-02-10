import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAssessmentComponent } from './show-assessment.component';

describe('ShowAssessmentComponent', () => {
  let component: ShowAssessmentComponent;
  let fixture: ComponentFixture<ShowAssessmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowAssessmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

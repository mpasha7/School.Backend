import { TestBed } from '@angular/core/testing';

import { AppliesService } from './applies.service';

describe('AppliesService', () => {
  let service: AppliesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppliesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

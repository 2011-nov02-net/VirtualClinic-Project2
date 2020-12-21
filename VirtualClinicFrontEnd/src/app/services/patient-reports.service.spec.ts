import { TestBed } from '@angular/core/testing';

import { PatientReportsService } from './patient-reports.service';

describe('PatientReportsService', () => {
  let service: PatientReportsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientReportsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

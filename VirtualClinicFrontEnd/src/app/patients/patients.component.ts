import { Component, OnInit } from '@angular/core';
import { Patient } from '../models/patient';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

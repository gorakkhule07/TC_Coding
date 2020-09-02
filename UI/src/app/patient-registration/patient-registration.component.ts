import { Component, OnInit } from '@angular/core';
import { PatientService } from '../Patient-service/patient.service';

@Component({
  selector: 'app-patient-registration',
  templateUrl: './patient-registration.component.html',
  styleUrls: ['./patient-registration.component.scss']
})
export class PatientRegistrationComponent implements OnInit {

  constructor(private PatientService: PatientService) { }

  public Model: any = { patient: {}, cities: [], states: [] };
  public PatientList: any;
  public operation = 'Save';
  public sucessmessage ='';
  public userMessages=[];

  ngOnInit(): void {
    // Get patient list
    this.GetPatients();
    //get state list
    this.PatientService.GetStates().subscribe(response => {
      this.Model.states = response;
    })

  }
  GetPatients() {
    this.PatientService.GetAllPatients().subscribe(response => {
      this.PatientList = response;
    });
  }
  GetCities() {

    this.PatientService.GetCities(this.Model.stateid).subscribe(response => {
      this.Model.cities = response;
    })
  }
  FetchRecord(id) {

    this.PatientService.GetPatient(id).subscribe(response => {
      this.Model = response;
      this.operation = 'Update';
    });
  }
  SavePatient() {
    this.sucessmessage ='';
    this.userMessages=[];
    if (
      this.Model.patient.name == '' || this.Model.patient.name == undefined ||
      this.Model.patient.surName == '' || this.Model.patient.surName == undefined ||
      this.Model.patient.dob == null || this.Model.patient.dob == undefined ||
      this.Model.patient.gender == 0 || this.Model.patient.gender == undefined ||
      this.Model.patient.cityId == 0 || this.Model.patient.cityId == undefined ||
      this.Model.stateid == 0 || this.Model.stateid == undefined
    ) {
      return;
    }
    else {
      this.PatientService.SavePatient(this.Model).subscribe((Response: any) => {
        this.userMessages = Response.userMessages;
        if (Response.result == false && Response.userMessages.length == 0) {
          console.log(this.userMessages);
          this.Cancel();
          this.GetPatients();
          this.sucessmessage='Record saved successfully';
        }
      })
    }



  }
  Cancel() {
    this.operation = 'Save';
    this.Model.patient.name = '';
    this.Model.patient.surName = '';
    this.Model.patient.dob = null;
    this.Model.patient.gender = 0;
    this.Model.patient.cityId = 0;
    this.Model.stateid = 0;
    this.Model.patient.cities = [];
  }

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) { }

  GetAllPatients() {
    console.log(environment.apiurl + 'Patient/GetAllPatients');
    return this.http.get(environment.apiurl + 'Patient/GetAllPatients');
  }
  GetPatient(id) {
    return this.http.get<any>(environment.apiurl + 'Patient/GetPatient?id=' + id);
  }
  GetStates() {
    return this.http.get(environment.apiurl + 'Patient/GetStates');
  }
  GetCities(StateId) {
    return this.http.get(environment.apiurl + 'Patient/GetCitiesByStateId?stateid=' + StateId);
  }
  SavePatient(model) {
    console.log(model);
    return this.http.post(environment.apiurl + 'Patient/RegisterPatient',model);

  }

}

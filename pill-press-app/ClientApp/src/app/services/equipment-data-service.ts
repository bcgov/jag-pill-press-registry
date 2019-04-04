import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipment } from '../models/equipment.model';
import { DynamicsAccount } from '../models/dynamics-account.model';

@Injectable()
export class EquipmentDataService {

  apiPath = 'api/equipment/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  temp: Observable<any>;

  constructor(private http: HttpClient) { }

  /**
   * Get equipment by Id
   * @param equipmentId
   */
  public getEquipment(equipmentId: string): Observable<Equipment> {
    return this.http.get<Equipment>(this.apiPath + equipmentId, { headers: this.headers });
  }

  /**
   * Change Equipment Location
   * @param applicationData
   */
  public changeEquipmentLocation(applicationData: any): Observable<Equipment> {
    var temp = applicationData;
    //return temp;
    return this.http.put<Equipment>(this.apiPath + applicationData.id + '/changeEquipmentLocation', applicationData, { headers: this.headers });
  }

}

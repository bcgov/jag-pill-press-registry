import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipment } from '../models/equipment.model';
import { DynamicsAccount } from '../models/dynamics-account.model';
import { Application } from '../models/application.model';
import { EquipmentLocation } from '../models/equipment-location.model';

@Injectable()
export class EquipmentDataService {

  apiPath = 'api/equipment/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  temp: Observable<any>;
  equipmentLocationData: EquipmentLocation;

  constructor(private http: HttpClient) { }

  /**
   * Get equipment by Id
   * @param equipmentId
   */
  public getEquipment(equipmentId: string): Observable<Equipment> {
    return this.http.get<Equipment>(this.apiPath + equipmentId, { headers: this.headers });
  }

  public getEquipmentlocation(equipmentId: string, locationId: string): Observable<EquipmentLocation> {
    return this.http.get<EquipmentLocation>(this.apiPath + equipmentId + '/' + locationId, { headers: this.headers });
  }

  /**
   * Change Equipment Location
   * @param applicationData
   */
  public changeEquipmentLocation(applicationData: Application): Observable<Equipment> {
    return this.http.put<Equipment>(this.apiPath + applicationData.equipmentRecord.id + '/changeEquipmentLocation', applicationData, { headers: this.headers });
  }

}

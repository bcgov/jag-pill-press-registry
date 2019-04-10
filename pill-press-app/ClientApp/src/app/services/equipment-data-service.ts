import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipment } from '../models/equipment.model';
import { DynamicsAccount } from '../models/dynamics-account.model';
import { Application } from '../models/application.model';
import { EquipmentLocation } from '../models/equipment-location.model';
import { EquipmentLocationComponent } from '@app/equipment-notification/equipment-location/equipment-location.component';

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

  /**
   * Change Equipment Location
   * @param applicationData
   */
  public changeEquipmentLocation(applicationData: Application): Observable<Equipment> {

    //this.equipmentLocationData.privateDwelling = applicationData.equipmentLocation.privateDwelling;
    //this.equipmentLocationData.settingDescription = applicationData.equipmentLocation.settingDescription;
    //this.equipmentLocationData.fromWhen = new Date();
    //this.equipmentLocationData.equipment = applicationData.equipmentRecord;
    //this.equipmentLocationData.location.address = applicationData.equipmentLocation.address;
    //this.equipmentLocationData.location.privateDwelling = applicationData.equipmentLocation.privateDwelling;
    //this.equipmentLocationData.location.name = applicationData.equipmentLocation.name;
    //this.equipmentLocationData.location.id = applicationData.equipmentLocation.id;

    //return temp;
    //return this.http.put<Equipment>(this.apiPath + this.equipmentLocationData.equipment.id + '/changeEquipmentLocation', this.equipmentLocationData, { headers: this.headers });
    return this.http.put<Equipment>(this.apiPath + applicationData.equipmentRecord.id + '/changeEquipmentLocation', applicationData, { headers: this.headers });

  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipment } from '../models/equipment.model';
import { DynamicsAccount } from '../models/dynamics-account.model';

@Injectable()
export class EquipmentDataService {

  apiPath = 'api/equipment/';
  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });


  constructor(private http: HttpClient) { }

  /**
   * Get equipment by Id
   * @param equipmentId
   */
  public getEquipment(equipmentId: string): Observable<Equipment> {
    return this.http.get<Equipment>(this.apiPath + equipmentId, { headers: this.headers });
  }

}

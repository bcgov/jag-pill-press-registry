import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';


import { DynamicsForm } from '../models/dynamics-form.model';
import { DynamicsFormTab } from '../models/dynamics-form-tab.model';
import { DynamicsFormSection } from '../models/dynamics-form-section.model';
import { DynamicsFormField } from '../models/dynamics-form-field.model';
import { DynamicsFormFieldOption } from '../models/dynamics-form-field-option.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable()
export class DynamicsDataService {
  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  getForm(id: string) {
    return this.http.get('api/systemform/' + id, {
      headers: this.headers
    })
      .toPromise()
      .then((data: DynamicsForm) => {
        return data;
      })
      .catch(this.handleError);
  }

  // load a record from Dynamics.
  getRecord(entity: string, recordId: string) {
    return this.http.get('api/' + entity + '/' + recordId, {
      headers: this.headers
    });
  }

  createRecord(entity: string, data: any) {
    return this.http.post('api/' + entity, data, {
      headers: this.headers
    });
  }


  updateRecord(entity: string, id: string, data: any) {
    return this.http.put('api/' + entity + '/' + id, data, {
      headers: this.headers
    });
  }

  deleteRecord(entity: string, id: string) {
    return this.http.post(`api/${entity}/${id}/delete`, {
      headers: this.headers
    });
  }


  private handleError(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Promise.reject(errMsg);
  }
}

import { Injectable, Inject } from '@angular/core';

import { HttpHeaders, HttpClient } from '@angular/common/http';

import { VersionInfo } from '../models/version-info.model';

@Injectable()
export class VersionInfoDataService {

  apiPath = 'api/ApplicationVersionInfo/';
  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  public getVersionInfo() {
    return this.http.get<VersionInfo>(this.apiPath, { headers: this.headers });
  }
}

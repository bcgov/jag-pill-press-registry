import { Component, OnInit, Inject } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { VersionInfo } from '../models/version-info.model';
import * as moment from 'moment';

@Component({
  selector: 'app-version-info',
  templateUrl: './version-info.component.html',
  styleUrls: ['./version-info.component.scss']
})
export class VersionInfoDialog implements OnInit {
  public versionInfo: VersionInfo;

  constructor(public dialogRef: MatDialogRef<VersionInfoDialog>,
    @Inject(MAT_DIALOG_DATA) public data: VersionInfo) { 
      this.versionInfo = data;
      this.versionInfo.fileCreationTime = moment(this.versionInfo.fileCreationTime, 'YYYY-MM-DDTHH:mm:ss.SSSSSZ')
        .format('DD-MMM-YYYY hh:mm:ss')
    }

  closeDialog() {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

}

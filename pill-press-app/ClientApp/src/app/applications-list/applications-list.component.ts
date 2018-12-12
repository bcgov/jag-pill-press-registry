import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Application } from '../models/application.model';
import { ApplicationDataService } from '../services/adoxio-application-data.service';
import { MatPaginator, MatTableDataSource, MatSort} from '@angular/material';

@Component({
  selector: 'app-applications-list',
  templateUrl: './applications-list.component.html',
  styleUrls: ['./applications-list.component.css']
})
export class ApplicationsListComponent implements OnInit {
  // Applications: Application[];

  displayedColumns = ['name', 'applyingPerson', 'jobNumber', 'licenseType'];
  dataSource = new MatTableDataSource<Application>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  constructor(private ApplicationDataService: ApplicationDataService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.ApplicationDataService.getApplications()
      .subscribe((data: Application[]) => {
        this.dataSource.data = data;
      });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}

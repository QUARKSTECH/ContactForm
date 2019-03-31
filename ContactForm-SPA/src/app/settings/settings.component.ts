import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  tenant: any = {
    extraProps: {}
  };
  tenants: any = [];
  tempTenants: any = [
    // extraProps: {}
  ];

  constructor(private http: HttpClient,  private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.getTenants();
  }

  saveTenant() {
    if (this.tenant.mobile) {
        const mobileNumber = this.tenant.mobile.split(',');
        this.tenant.extraProps.mobileNumber = mobileNumber;
        this.http.post(environment.apiUrl + 'settings', this.tenant).subscribe(
        response => {
            this.tenant = {
              extraProps: {}
            };
            this.alertify.success('Tenant added successfully');
        },
        error => {
          this.alertify.error(error);
        }
      );
    } else {
      this.alertify.error('Please add mobile number');
    }
  }

  getTenants() {
    this.http.get(environment.apiUrl + 'settings').subscribe(
      response => {
        this.tempTenants = response;
        let counter = 0;
        this.tempTenants.forEach(element => {
              this.tenants.push(element.extraProps);
              this.tenants[counter].id = element.id;
              this.tenants[counter].email = element.email;
              this.tenants[counter].shortName = element.shortName;
              this.tenants[counter].mobile = element.extraProps.mobileNumber === undefined ? '' : element.extraProps.mobileNumber.join();
              counter++;
        });
        if (this.tenants.length) {
          this.alertify.success('Tenants loaded successfully');
        }
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}

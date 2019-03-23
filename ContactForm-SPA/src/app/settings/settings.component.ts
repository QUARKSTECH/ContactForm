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

  tenant: any = {};
  constructor(private http: HttpClient,  private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  saveTenant() {
    this.http.post(environment.apiUrl + 'settings', this.tenant).subscribe(
      response => {
          this.tenant = {};
          this.alertify.success('Tenant added successfully');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}

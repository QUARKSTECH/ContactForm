import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from '../_service/alertify.service';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-enquiries',
  templateUrl: './enquiries.component.html',
  styleUrls: ['./enquiries.component.css']
})
export class EnquiriesComponent implements OnInit {
  enquiries: any = [];
  tempEnquiries: any = [
    // extraProps: {}
  ];
  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getEnquiries();
  }

  getEnquiries() {
    this.http.get(environment.apiUrl + 'enquiry').subscribe(
      response => {
        this.tempEnquiries = response;
        // this.tempEnquiries.forEach(function(item) {
        //     this.enquiries.push(item.extraProps);
        // });
        this.tempEnquiries.forEach(element => {
              this.enquiries.push(element.extraProps);
        });
        if (this.enquiries.length) {
          this.alertify.success('Enquiries loaded successfully');
        }
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}

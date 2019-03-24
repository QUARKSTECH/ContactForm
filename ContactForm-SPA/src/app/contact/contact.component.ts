import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from '../_service/alertify.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  contact: any = {
    extraProps: {},
    tenantId: 1
  };
  baseurl  =  environment.apiUrl + 'enquiry/';
  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  addContact() {
    this.http.post(this.baseurl, this.contact).subscribe(
      response => {
        this.alertify.success('Enquiry saved successfully');
        this.contact.extraProps = {};
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}

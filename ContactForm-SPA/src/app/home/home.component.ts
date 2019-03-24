import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  jwtHelper = new JwtHelperService();

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }
  model: any = {};
  showRegister: any = false;
  showOtp: any = false;
  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
      if (this.authService.decodedToken.unique_name === 'admin') {
        this.router.navigate(['/admin']);
      } else {
        this.router.navigate(['/home']);
      }
    }, error => {
      this.alertify.error(error);
    });
  }

  smsLogin() {
    if (!this.showRegister) {
      this.authService.loginMobileNumber(this.model).subscribe(next => {
        if (next) {
          if (next.token) {
            localStorage.setItem('token', next.token);
            this.authService.decodedToken = this.jwtHelper.decodeToken(next.token);
            this.router.navigate(['/contact']);
            this.alertify.success('Logged in successfully');
          } else {
            this.showOtp = true;
          }
        } else {
          this.showRegister = true;
        }
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.authService.register(this.model).subscribe(next => {
        this.showRegister = false;
      }, error => {
        this.alertify.error(error);
      });
    }
  }



}

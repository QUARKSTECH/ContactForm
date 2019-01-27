import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }
  model: any = {};
  showRegister: any = false;
  showOtp: any = false;
  ngOnInit() {
    this.loggedIn();
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
          if (next.data) {
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

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out');
    this.model = {};
    this.router.navigate(['/home']);
  }

}

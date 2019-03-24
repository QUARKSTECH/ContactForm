import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule } from '@angular/forms';
import { appRoutes } from './routes';
import { TabsModule } from 'ngx-bootstrap';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { AuthService } from './_service/auth.service';
import { ErrorInterceptorProvider } from './_service/error.interceptor';
import { AlertifyService } from './_service/alertify.service';
import { JwtModule } from '@auth0/angular-jwt';
import { EnquiriesComponent } from './enquiries/enquiries.component';
import { SettingsComponent } from './settings/settings.component';
import {DataTableModule} from 'angular-6-datatable';

export function tokenGetter() {
  return localStorage.getItem('token');
}
@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      ContactComponent,
      HeaderComponent,
      FooterComponent,
      NavmenuComponent,
      EnquiriesComponent,
      SettingsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes),
      TabsModule.forRoot(),
      DataTableModule,
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

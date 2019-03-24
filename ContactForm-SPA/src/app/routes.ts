import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { EnquiriesComponent } from './enquiries/enquiries.component';
import { SettingsComponent } from './settings/settings.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {
      path: '',
      canActivateChild: [AuthGuard],
      children: [
        { path: 'home', component: HomeComponent, data: {}},
        { path: 'contact', component: ContactComponent, data: {} },
        { path: 'enquiries', component: EnquiriesComponent, data: { allowedRoles: ['admin'] } },
        { path: 'settings', component: SettingsComponent, data: { allowedRoles: ['admin'] } },
        { path: '**', redirectTo: 'home', pathMatch: 'full'}
      ]
    }
  ];

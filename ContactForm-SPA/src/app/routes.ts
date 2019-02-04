import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { EnquiriesComponent } from './enquiries/enquiries.component';
import { SettingsComponent } from './settings/settings.component';


export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'contact', component: ContactComponent },
    { path: 'enquiries', component: EnquiriesComponent },
    { path: 'settings', component: SettingsComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full'}
];

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      ContactComponent
   ],
   imports: [
      BrowserModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

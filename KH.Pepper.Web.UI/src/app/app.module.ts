import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './components/general/about/about.component';
import { ContactUsComponent } from './components/general/contactus/contactus.component';
import { FooterComponent } from './components/general/footer/footer.component';
import { ForgotPasswordComponent } from './components/general/forgot-password/forgot-password.component';
import { HeaderComponent } from './components/general/header/header.component';
import { LoginComponent } from './components/general/login/login.component';
import { PageNotFoundComponent } from './components/general/page-not-found/page-not-found.component';
import { CacheMapService } from './services/cache/cache-map.service';
import { HomeComponent } from './components/general/home/home.component';
import { DashboardComponent } from './components/general/dashboard/dashboard.component';
import { RegisterComponent } from './components/general/register/register.component';

@NgModule({
  declarations: [
    AppComponent ,
    HomeComponent,   
    LoginComponent,
    AboutComponent,
    ContactUsComponent,
    ForgotPasswordComponent,
    PageNotFoundComponent, 
    HeaderComponent,
    FooterComponent ,
    DashboardComponent,    
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

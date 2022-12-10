import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactUsComponent } from './components/general/contactus/contactus.component';
import { DashboardComponent } from './components/general/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/general/forgot-password/forgot-password.component';
import { HomeComponent } from './components/general/home/home.component';
import { LoginComponent } from './components/general/login/login.component';
import { PageNotFoundComponent } from './components/general/page-not-found/page-not-found.component';
import { ProductComponent } from './components/general/product/product.component';
import { RegisterComponent } from './components/general/register/register.component';
 
const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent,title:'KolliHills Pepper' },
  { path: 'dashboard', component: DashboardComponent  },
  { path: 'login', component: LoginComponent, },
  { path: 'register', component: RegisterComponent, },
  { path: 'forgot-password', component: ForgotPasswordComponent, },
  { path: 'product', component: ProductComponent, },
  { path: 'contactus', component: ContactUsComponent, },

  //other default
  //admin
   
  // { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

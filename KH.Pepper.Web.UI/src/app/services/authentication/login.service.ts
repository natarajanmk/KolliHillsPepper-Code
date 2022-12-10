import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { UserDto } from '../api/models/userDto';
import { environment } from 'src/environments/environment';

const JWT_TOKEN_KEY = 'JWT_TOKEN';
const REFRESH_TOKEN_KEY = 'REFRESH_TOKEN';
const USER_KEY = 'auth-user';
 
//localStorage.getItem("token")
//localStorage.setItem('token', tokendata.jwtToken);

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private isloggedIn: boolean = false;
	private loggedInUser = {} as UserDto;
  readonly apiURL = environment.baseURL;
 
  constructor(private http: HttpClient, private router: Router) { }

  tokenresp: any;
  private _updatemenu = new Subject<void>();
  get updatemenu() {
    return this._updatemenu;
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post((this.apiURL + '/api/Account/UserAuthentication'), {
      username,
      password
    }, httpOptions);
  }

  GenerateRefreshToken() {
    let tokens = {
      "Token": this.GetToken(),
      "RefreshToken": this.GetRefreshToken()
    }
    return this.http.post((this.apiURL + '/api/Account/RefreshToken'), {tokens}, httpOptions);
  } 

  SaveTokens(tokendata: any) {
    window.sessionStorage.setItem(JWT_TOKEN_KEY, tokendata.Token);
    window.sessionStorage.setItem(REFRESH_TOKEN_KEY, tokendata.RefreshToken);
    const user = this.getUser();
    if (user.id) {
      this.saveUser({ user });
    }
  }  

  GetToken() {
    return window.sessionStorage.getItem(JWT_TOKEN_KEY) || '';
  }

  GetRefreshToken() {
    return window.sessionStorage.getItem(REFRESH_TOKEN_KEY) || '';
  }

  Proceddlogin(usercred: any) {
    return this.http.post(this.apiURL + 'authenticate', usercred);
  }
 
  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }
    return {};
  }
  
  getLoggedInUser(): UserDto {
		return this.loggedInUser;
	}
  IsLoggedIn() {
    return window.sessionStorage.getItem(JWT_TOKEN_KEY) != null;
  }
    
  signOut() {
    window.sessionStorage.clear();   
    this.router.navigate(['login']);
  }

  Logout() {    
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }

  logoutUser(): void {
		this.isloggedIn = false;
	}

}

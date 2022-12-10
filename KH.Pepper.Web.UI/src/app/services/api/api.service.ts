import { Injectable } from '@angular/core';
import {  HttpClient, HttpHeaders,HttpParams  } from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServices {

  constructor(private http: HttpClient ) { 
  }
   
  httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'  })}
  
  readonly apiURL = environment.baseURL + 'api';
  
  GetAll(routeName:any){
    return this.http.get<any[]>(this.apiURL + '/'+ routeName);
  }

  GetById(routeName:any, Id:any){
    return this.http.get<any>(this.apiURL + '/'+ routeName + Id);
  }

  Save(routeName:any,inputdata:any){
    return this.http.post(this.apiURL + '/'+ routeName,inputdata);
  }

  Delete(routeName:any,inputdata:any){
    return this.http.delete(this.apiURL + '/'+ routeName,inputdata);
  }

}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IToken } from '../models/token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  checkValidation() {
    this.isLogged.next(this.isAuthenticated)
  }
  get isAuthenticated() {
    return !!localStorage.getItem('token');
  }

  constructor(private http: HttpClient,private router:Router) {
  }

  logout(){
    localStorage.removeItem('token')
    this.checkValidation()
    this.router.navigate(['login'])
  }

  isLogged: Subject<boolean> = new Subject<boolean>();

  authAsync(userName: string, password: string): Observable<IToken> {
    return this.http.post<IToken>(`${environment.apiUrl}/auth`, { userName, password });
  }
}

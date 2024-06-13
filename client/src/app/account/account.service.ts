import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address, User } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string | null) {
    if (token == null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<User>(this.baseUrl + 'account', {headers}).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          return null;
        }
      })
    )
  }

  /*login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }*/
    login(values: any) {
      return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
        map(user => {
          localStorage.setItem('token', user.token);
          var role = this.getRoleFromToken(user.token);
          console.log('User role from token:*******************', this.getRoleFromToken(user.token));
          this.currentUserSource.next(user);
          localStorage.setItem('role', user.role);
          document.cookie = `token=${user.token}`;
          document.cookie = 'role=' + user.role;
          if (role === 'Admin') {
            console.log('User role:admin*******************');
            this.router.navigateByUrl('/admin');
          }
        })
      )
    }
    hasRoleAdmin(): boolean {
      
      const token = document.cookie.split('; ').find(row => row.startsWith('token='));
      if (token == null) {
        return false;
      }
      const tokenValue = token ? token.split('=')[1] : null;
    
      console.log('User token:tokenvalue*******************', tokenValue);
      const role = this.getRoleFromToken(tokenValue);
      return role === 'Admin';
    }

    getRoleFromToken(token: any) {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const payload = JSON.parse(atob(base64));
   
      return payload.role;
  }

  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    const cookies = document.cookie.split(";");
 
    for (let i = 0; i < cookies.length; i++) {
    const cookie = cookies[i];
    const eqPos = cookie.indexOf("=");
    const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/emailExists?email=' + email);
  }

  getUserAddress() {
    return this.http.get<Address>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: Address) {
    return this.http.put(this.baseUrl + 'account/address', address);
  }
}
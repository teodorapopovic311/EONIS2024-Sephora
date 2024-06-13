import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })
  returnUrl: string;

  /*constructor(private accountService: AccountService, private router: Router, 
    private activatedRoute: ActivatedRoute) {
      this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop'
  }*/
      constructor(private accountService: AccountService, private router: Router,
        private activatedRoute: ActivatedRoute) {
          //get user role from local storage
          const role = JSON.parse(localStorage.getItem('role') || '{}');
          console.log('User role:***************************', role);


         
          //redirect to admin page if user role is admin
          if (role === 'admin') {
            // this.router.navigateByUrl('/admin');
            this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/admin'
          }
          this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/admin'
      }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl)
    })
  }
}
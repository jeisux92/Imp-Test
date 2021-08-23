import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      userName: ['admin@admin.com', [Validators.required]],
      password: ['AdminAdmin123.', [Validators.required]],
    })
  }

  async login() {
    let userName: string = this.form.get('userName').value;
    let password: string = this.form.get('password').value;
    try {

      const response = await this.authService.authAsync(userName, password).toPromise();
      localStorage.setItem('token', response.access_token);
      this.authService.isLogged.next(true)
      this.router.navigate(['products'])
    } catch (err) {


    }
  }


}

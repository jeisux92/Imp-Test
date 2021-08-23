import { Component } from '@angular/core';
import { AuthService } from '../core/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  opened: boolean = true
  isLogged: boolean
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    
    this.authService.isLogged.subscribe((v: boolean) => {

      this.isLogged = v
    })
    this.authService.checkValidation()
  }

  logout(){
    this.authService.logout()
  }
}

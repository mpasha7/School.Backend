import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/services/auth/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent implements OnInit {
  // public isUserAuthenticated: boolean = false;

  constructor(private _authService: AuthService) {}

  ngOnInit(): void {
    // this._authService.loginChanged.subscribe(result => {
    //   this.isUserAuthenticated = result;
    // });
    // if (this.isUserAuthenticated) {
    //   this._authService.logout();
    // }

    this._authService.logout();
  }
}

import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from './core/services/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'Gymnastyc School';
  public userAuthenticated  = false;

  constructor(private _authService: AuthService) {
    this._authService.loginChanged.subscribe(result => {
      this.userAuthenticated = result;
    });
  }

  ngOnInit(): void {
    this._authService.isAuthenticated().then(result => {
      this.userAuthenticated = result;
    });
  }
}

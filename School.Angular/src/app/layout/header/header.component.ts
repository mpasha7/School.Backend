import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { DOCUMENT } from '@angular/common';
import { Constants } from '../../shared/constants/constants';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  public isUserAuthenticated: boolean = false;
  // public isUserAdmin: boolean = false;
  public userRole: string = '';

  constructor (
    private _authService: AuthService,
    @Inject(DOCUMENT) private document: Document
  ) {}

  ngOnInit(): void {
    this._authService.loginChanged.subscribe(result => {
      this.isUserAuthenticated = result;
      this.getUserRole();
      // this.isAdmin();
    });
  }

  public login = () => {
    this._authService.login();
  }

  public logout = () => {
    this._authService.logout();
  }

  public getUserRole = () => {
    return this._authService.getUserRole().then(role => {
      this.userRole = role;
    });
  }

  public goToAdminPanel = (localPath: string) => {
    this.document.location.href = Constants.idpAuthority + localPath;
  }

  // public isAdmin = () => {
  //   return this._authService.checkIfUserIncludeInRoles(['Admin']).then(result => {
  //     this.isUserAdmin = result;
  //   });
  // }
}

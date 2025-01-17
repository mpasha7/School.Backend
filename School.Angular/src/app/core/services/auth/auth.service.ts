import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { Subject } from 'rxjs';
import { Constants } from '../../../shared/constants/constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _userManager: UserManager;
  private _user!: User | null;

  private _loginChangedSubject = new Subject<boolean>();
  public loginChanged = this._loginChangedSubject.asObservable();

  constructor() { 
    this._userManager = new UserManager(this.idpSettings);
    this._userManager.events.addAccessTokenExpired(_ => {
      this._loginChangedSubject.next(false);
    });
  }

  private get idpSettings(): UserManagerSettings {
    return {
      authority: Constants.idpAuthority,
      client_id: Constants.clientId,
      redirect_uri: `${Constants.clientRoot}/signin-callback`,
      scope: 'openid profile email phone roles SchoolWebApi',
      response_type: 'code',
      post_logout_redirect_uri: `${Constants.clientRoot}/signout-callback`,
      // refresh
      automaticSilentRenew: true,
      silent_redirect_uri: `${Constants.clientRoot}/assets/silent-callback.html`
    }
  }

  public login = () => {
    return this._userManager.signinRedirect();
  }

  public isAuthenticated = (): Promise<boolean> => {
    return this._userManager.getUser().then(user => {
      if (this._user !== user) {
        this._loginChangedSubject.next(this.checkUser(user));
      }

      this._user = user;
      return this.checkUser(user);
    });
  }

  private checkUser = (user: User | null): boolean => {
    return !!user && !user.expired;
  }

  public finishLogin = (): Promise<User> => {
    return this._userManager.signinRedirectCallback().then(user => {
      this._user = user;
      this._loginChangedSubject.next(this.checkUser(user));
      return user;
    });
  }

  public logout = () => {
    this._userManager.signoutRedirect();
  }

  public finishLogout = () => {
    this._user = null;
    this._loginChangedSubject.next(false);
    return this._userManager.signoutRedirectCallback();
  }

  public getAccessToken = (): Promise<string | null> => {
    return this._userManager.getUser().then(user => {
      return !!user && !user.expired ? user.access_token : null;
    });
  }

  public checkIfUserIncludeInRoles = (checkRoles: string[]) => {
    return this._userManager.getUser().then(user => {
      return checkRoles.includes(user?.profile['role']);
    });
  }

  public getUserRole = (): Promise<string> => {
    return this._userManager.getUser().then(user => {
      return user?.profile['role'];
    });
  }
}

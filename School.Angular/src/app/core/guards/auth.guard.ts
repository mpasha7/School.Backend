import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private _authService: AuthService,
    private _router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    const roles = route.data['roles'] as Array<string>;
    if (!roles) {
      return this.checkIsUserAuthenticated();
    }
    else {
      return this.checkForRoles(roles);
    }
  }

  private checkIsUserAuthenticated() {
    return this._authService.isAuthenticated().then(result => {
      return result ? true : this.redirectToUnauthorized();
    });
  }

  private checkForRoles(roles: string[]) {
    return this._authService.checkIfUserIncludeInRoles(roles).then(result => {
      return result ? true : this.redirectToUnauthorized();
    });
  }

  private redirectToUnauthorized() {
    this._router.navigate(['/unauthorized']);
    return false;
  }
}

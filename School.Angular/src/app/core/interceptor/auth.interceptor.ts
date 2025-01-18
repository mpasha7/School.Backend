import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';
import { catchError, from, lastValueFrom, Observable } from 'rxjs';
import { Constants } from '../../shared/constants/constants';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private _authService: AuthService,
    private _router: Router
  ) { }

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (req.url.startsWith(Constants.apiRoot) && !req.url.startsWith(Constants.apiRoot + '/home')) {
      return from(this._authService.getAccessToken().then(async token => {
        const headers = req.headers.set('Authorization', `Bearer ${token}`);
        const authRequest = req.clone({ headers });

        return await lastValueFrom(next.handle(authRequest).pipe(
          catchError((error: HttpErrorResponse) => {
            if (error && (error.status === 401 || error.status === 403)) {
              this._router.navigate(['/unauthorized']);
            }
            throw "Error in a request " + error.status;
          })
        ));
      }));
    }
    else {
      return next.handle(req);
    }
  }
}

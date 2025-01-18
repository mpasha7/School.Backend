import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin-redirect-callback',
  templateUrl: './signin-redirect-callback.component.html',
  styleUrl: './signin-redirect-callback.component.css'
})
export class SigninRedirectCallbackComponent implements OnInit {
  // redirecrUrl: string = '';

  constructor(
    private _authService: AuthService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    // this._authService.getUserRole().then(role => {
    //   this.redirecrUrl = (role == 'Coach' || role == 'Student') ? '/courses/list' : '/';
    // });

    this._authService.finishLogin().then(_ => {
      this._router.navigate(['/'], { replaceUrl: true });
    });
  }
}

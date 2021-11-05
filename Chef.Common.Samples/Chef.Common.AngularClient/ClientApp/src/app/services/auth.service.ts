import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private manager = new UserManager(getClientSettings());
  private user: User = null;

  constructor(private router: Router) {
    this.manager.getUser().then(user => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(route: string): Promise<void> {
    return this.manager.signinRedirect({ state: '/' + route });
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
      if (user.state) {
        this.router.navigate([user.state]);
      }
    });
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'https://localhost:9001/',
    client_id: 'AngularClient',
    redirect_uri: 'https://localhost:9003/auth-callback',
    post_logout_redirect_uri: 'https://localhost:9003/',
    response_type: 'code',
    scope: 'openid profile webApi',
    filterProtocolClaims: true,
    loadUserInfo: true
  };
}
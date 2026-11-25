
import { Injectable, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, tap, catchError, map, finalize } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import {
  LoginCommand,
  LoginCommandDto,
  LogoutCommand,
  RefreshTokenCommand,
  RefreshTokenCommandDto,
} from '../../../api-services/auth/auth-api.model';

import { AuthStorageService } from './auth-storage.service';
import { CurrentUserDto } from './current-user.dto';
import { JwtPayloadDto } from './jwt-payload.dto';


@Injectable({ providedIn: 'root' })
export class AuthFacadeService {
  private api = inject(AuthApiService);
  private storage = inject(AuthStorageService);
  private router = inject(Router);

  // === REACTIVE STATE: current user ===

  private _currentUser = signal<CurrentUserDto | null>(null);


  currentUser = this._currentUser.asReadonly();


  isAuthenticated = computed(() => !!this._currentUser());
  isAdmin = computed(() => this._currentUser()?.isAdmin ?? false);
  isManager = computed(() => this._currentUser()?.isManager ?? false);
  isPublicUser = computed(() => this._currentUser()?.isPublicUser ?? false);

  constructor() {

    this.initializeFromToken();
  }

  // =========================================================
  // PUBLIC API
  // =========================================================


  login(payload: LoginCommand): Observable<void> {
    return this.api.login(payload).pipe(
      tap((response: LoginCommandDto) => {
        this.applyAuthBundle(response);
      }),
      map(() => void 0),
    );
  }


  logout(): Observable<void> {
    const refreshToken = this.storage.getRefreshToken();


    this.clearUserState();

    if (!refreshToken) {
      this.clearUserState();
      return of(void 0);
    }

    const payload: LogoutCommand = { refreshToken };


    return this.api.logout(payload).pipe(
      catchError(() => of(void 0)),
      finalize(() => this.clearUserState()),
      map(() => void 0),
    );
  }


  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.api.refresh(payload).pipe(
      tap((response: RefreshTokenCommandDto) => {
        this.applyAuthBundle(response);
      }),
    );
  }

  applyAuthBundle(response: LoginCommandDto | RefreshTokenCommandDto): void {


    this.storage.saveLogin(response);
    this.decodeAndSetUser(response.accessToken);
  }


  redirectToLogin(): void {
    this.clearUserState();
    this.router.navigate(['/auth/login']);
  }


  getAccessToken(): string | null {
    return this.storage.getAccessToken();
  }


  getRefreshToken(): string | null {
    return this.storage.getRefreshToken();
  }




  private initializeFromToken(): void {
    const token = this.storage.getAccessToken();
    if (token) {
      this.decodeAndSetUser(token);
    }
  }

  private decodeAndSetUser(token: string): void {
    try {
      const payload = jwtDecode<JwtPayloadDto>(token);

      if (this.isTokenExpired(payload)) {
        this.clearUserState();
        return;
      }


      const user: CurrentUserDto = {
        userId: Number(payload.sub),
        email: payload.email,
        isAdmin: payload.is_admin === 'true',
        isManager: payload.is_manager === 'true',
        isPublicUser: payload.is_public_user === 'true',
        tokenVersion: Number(payload.ver),
      };

      this._currentUser.set(user);
    } catch (error) {
      console.error('Failed to decode JWT token:', error);
      this._currentUser.set(null);
    }
  }
  private isTokenExpired(payload: JwtPayloadDto): boolean {
    if (!payload.exp) {
      return true;
    }

    const currentUnixTime = Math.floor(Date.now() / 1000);

    return payload.exp <= currentUnixTime;
  }


  private clearUserState(): void {
    this._currentUser.set(null);
    this.storage.clear();
  }
}

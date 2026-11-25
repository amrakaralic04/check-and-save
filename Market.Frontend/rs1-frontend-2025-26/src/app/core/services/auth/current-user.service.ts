
import { Injectable, inject, computed } from '@angular/core';
import { AuthFacadeService } from './auth-facade.service';

@Injectable({ providedIn: 'root' })
export class CurrentUserService {
  private auth = inject(AuthFacadeService);

  /** Signal koji UI može čitati (readonly) */
  currentUser = computed(() => this.auth.currentUser());

  isAuthenticated = computed(() => this.auth.isAuthenticated());
  isAdmin = computed(() => this.auth.isAdmin());
  isManager = computed(() => this.auth.isManager());
  isPublicUser = computed(() => this.auth.isPublicUser());

  get snapshot() {
    return this.auth.currentUser();
  }


  getDefaultRoute(): string {
    const user = this.snapshot;
    if (!user) return '/auth/login';


    if (user.isAdmin) return '/admin';
    return '/client';
  }
}

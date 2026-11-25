import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { trigger, transition, style, animate } from '@angular/animations';
import { Subscription } from 'rxjs';

import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';

@Component({
  selector: 'app-logout',
  standalone: false,
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.scss',
  animations: [
    trigger('fadeInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(10px)' }),
        animate('400ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ])
  ]
})
export class LogoutComponent implements OnInit, OnDestroy {
  private router = inject(Router);
  private auth = inject(AuthFacadeService);
  private logoutSubscription?: Subscription;
  private countdownIntervalId?: ReturnType<typeof setInterval>;

  countdownSeconds = 2;

  ngOnInit(): void {
    this.logoutSubscription = this.auth.logout().subscribe({
      next: () => this.startCountdown(),
      error: () => this.startCountdown()
    });
  }
  ngOnDestroy(): void {
    this.logoutSubscription?.unsubscribe();

    if (this.countdownIntervalId) {
      clearInterval(this.countdownIntervalId);
      this.countdownIntervalId = undefined;
    }
  }

  private startCountdown(): void {
      if (this.countdownIntervalId) {
        return;
      }

      this.countdownIntervalId = setInterval(() => {
        this.countdownSeconds--;


        if (this.countdownSeconds <= 0 && this.countdownIntervalId) {
          clearInterval(this.countdownIntervalId);
          this.countdownIntervalId = undefined;
          this.router.navigate(['/auth/login'], { replaceUrl: true });
      }
    }, 1000);
  }
}

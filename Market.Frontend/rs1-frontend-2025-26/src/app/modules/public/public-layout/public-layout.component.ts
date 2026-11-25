import { Component, HostListener, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';

@Component({
  selector: 'app-public-layout',
  standalone: false,
  templateUrl: './public-layout.component.html',
  styleUrl: './public-layout.component.scss',
})
export class PublicLayoutComponent implements OnInit {
  currentYear: string = new Date().getFullYear().toString();
  showScrollTop = false;

  constructor(
    private router: Router,
    private currentUser: CurrentUserService
  ) {}

  ngOnInit(): void {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        window.scrollTo({ top: 0, behavior: 'instant' });
      });
  }

  @HostListener('window:scroll')
  onScroll(): void {
    this.showScrollTop = window.scrollY > 400;
  }

  scrollToTop(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  navigateCta(guestRoute: string): void {
    if (this.currentUser.isAuthenticated()) {
      this.router.navigate(['/client/home']);
    } else {
      this.router.navigate([guestRoute]);
    }
  }
}

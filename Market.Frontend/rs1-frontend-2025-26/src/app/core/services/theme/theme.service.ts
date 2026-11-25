import { DOCUMENT } from '@angular/common';
import { Injectable, computed, inject, signal } from '@angular/core';

export type AppTheme = 'light' | 'dark';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private readonly document = inject(DOCUMENT);
  private readonly storageKey = 'themePreference';
  private readonly darkThemeClass = 'dark-theme';
  private readonly theme = signal<AppTheme>(this.getSavedTheme());

  readonly currentTheme = this.theme.asReadonly();
  readonly isDarkTheme = computed(() => this.theme() === 'dark');

  constructor() {
    this.applyTheme(this.theme());
  }

  setTheme(theme: AppTheme): void {
    this.theme.set(theme);
    localStorage.setItem(this.storageKey, theme);
    this.applyTheme(theme);
  }

  toggleTheme(): void {
    this.setTheme(this.isDarkTheme() ? 'light' : 'dark');
  }

  private getSavedTheme(): AppTheme {
    const savedTheme = localStorage.getItem(this.storageKey);

    return savedTheme === 'dark' ? 'dark' : 'light';
  }

  private applyTheme(theme: AppTheme): void {
    const root = this.document.documentElement;
    root.classList.toggle(this.darkThemeClass, theme === 'dark');
    root.setAttribute('data-theme', theme);
  }
}

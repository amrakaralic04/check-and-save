import { Injectable, computed, signal } from '@angular/core';
import { FavouriteProductCardDto } from '../../../api-services/favourites/favourites-api.models';

export interface FavoriteTogglePayload {
  publicId?: string;
  price?: number | null;
  imageUrl?: string | null;
}

@Injectable({ providedIn: 'root' })
export class FavoritesService {

  private readonly storageKey = 'check-and-save-favorites';
  private readonly favoritesMap = signal<Map<string, FavouriteProductCardDto>>(this.loadStoredFavorites());

  readonly favorites = computed(() =>
    Array.from(this.favoritesMap().values()).sort((a, b) => a.name.localeCompare(b.name))
  );
  toggle(productName: string, payload?: FavoriteTogglePayload): void {
    const key = this.normalizeKey(productName);
    const next = new Map(this.favoritesMap());

    if (next.has(key)) {
      next.delete(key);
      this.setFavorites(next);
      return;
    }

    next.set(key, {
      id: this.createLocalId(productName),
      publicId: payload?.publicId ?? this.createLocalPublicId(productName),
      productEntityId: Number(payload?.publicId) || 0,
      name: productName,
      price: payload?.price ?? null,
      imageUrl: payload?.imageUrl ?? null
    });

    this.setFavorites(next);
  }

  isFavorite(productName: string): boolean {
    return this.favoritesMap().has(this.normalizeKey(productName));
  }
  hasPublicId(publicId: string): boolean {
    return this.favorites().some((item) => item.publicId === publicId);
  }

  removeByPublicId(publicId: string): void {
    const next = new Map(this.favoritesMap());
    const entry = Array.from(next.entries()).find(([, item]) => item.publicId === publicId);

    if (entry) {
      next.delete(entry[0]);
      this.setFavorites(next);
    }
  }
  clear(): void {
    this.setFavorites(new Map());
  }

  private setFavorites(next: Map<string, FavouriteProductCardDto>): void {
    this.favoritesMap.set(next);
    localStorage.setItem(this.storageKey, JSON.stringify(Array.from(next.entries())));
  }

  private loadStoredFavorites(): Map<string, FavouriteProductCardDto> {
    const stored = localStorage.getItem(this.storageKey);

    if (!stored) {
      return new Map();
    }

    try {
      return new Map(JSON.parse(stored));
    } catch {
      localStorage.removeItem(this.storageKey);
      return new Map();
    }
  }

  private normalizeKey(name: string): string {
    return name.trim().toLowerCase();
  }

  private createLocalPublicId(name: string): string {
    return `local-${name.toLowerCase().replace(/[^a-z0-9]+/g, '-')}`;
  }

  private createLocalId(name: string): number {
    const normalized = name.toLowerCase().replace(/[^a-z0-9]+/g, '');
    let hash = 0;

    for (let i = 0; i < normalized.length; i++) {
      hash = ((hash << 5) - hash) + normalized.charCodeAt(i);
      hash |= 0;
    }

    return Math.abs(hash);
  }
}

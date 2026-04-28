import { Component, computed, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { FavoritesService } from '../../../shared/services/favorites.services';
import { CurrentUserService } from '../../../../core/services/auth/current-user.service';

type CategoryKey =
  | 'popularno'
  | 'namirnice'
  | 'elektronika'
  | 'drogerija'
  | 'prodavnice'
  | 'akcije';

type Product ={
  id: number;
  name: string;
  bestStore: string;
  price: number;
  category: 'namirnice' | 'elektronika' | 'drogerija' | 'akcije';
};

type CategoryTab = {
  key: CategoryKey;
  label: string;
  subtitle: string;
};

type Store = {
  name: string;
  city: string;
  hours: string;
};

@Component({
  selector: 'app-client-home',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, MatIconModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  private favoritesService = inject(FavoritesService);
  private currentUser = inject(CurrentUserService);

  activeCategory: CategoryKey = 'popularno';
  private readonly storageKey = 'client-home-active-category';
  searchTerm = '';
  visibleProducts: Product[] = [];
  visibleStores: Store[] = [];
  compareSelection: Product[] = [];
  comparePair: { left: Product; right: Product } | null = null;


  categoryTabs: CategoryTab[] = [
    { key: 'popularno', label: 'Popularno', subtitle: 'Top ponude danas' },
    { key: 'namirnice', label: 'Namirnice', subtitle: 'Svježe i osnovno' },
    { key: 'elektronika', label: 'Elektronika', subtitle: 'Uređaji i oprema' },
    { key: 'drogerija', label: 'Drogerija', subtitle: 'Njega i higijena' },
    { key: 'prodavnice', label: 'Prodavnice', subtitle: 'Lokacije i radno vrijeme' },
    { key: 'akcije', label: 'Akcije', subtitle: 'Aktuelni popusti' },
  ];

  ngOnInit(): void {
    const storedCategory = localStorage.getItem(this.storageKey);
    if (storedCategory && this.isCategoryKey(storedCategory)) {
      this.activeCategory = storedCategory;
    }
    this.updateActiveContent();
  }

  stores: Store[] = [
    { name: 'Prodavnica 1', city: 'Mostar', hours: '07:00 - 21:00' },
    { name: 'Prodavnica 2', city: 'Mostar', hours: '08:00 - 22:00' },
    { name: 'Prodavnica 3', city: 'Mostar', hours: '07:30 - 20:30' },
    { name: 'Prodavnica 4', city: 'Mostar', hours: '08:00 - 21:30' },
    { name: 'Prodavnica 5', city: 'Mostar', hours: '09:00 - 20:00' },
  ];

  popularProducts: Product[] = [
    { id: 1, name: 'Mlijeko', bestStore: 'Prodavnica 2', price: 2.45, category: 'namirnice' },
    { id: 2, name: 'Tastatura', bestStore: 'Prodavnica 2', price: 39.9, category: 'elektronika' },
    { id: 3, name: 'Šampon', bestStore: 'Prodavnica 3', price: 6.8, category: 'drogerija' },
    { id: 4, name: 'Jaja', bestStore: 'Prodavnica 1', price: 3.1, category: 'namirnice' },
    { id: 5, name: 'Miš', bestStore: 'Prodavnica 5', price: 19.9, category: 'elektronika' },
    { id: 6, name: 'Sapun', bestStore: 'Prodavnica 5', price: 2.1, category: 'drogerija' },
  ];

  groceriesProducts: Product[] = [
    { id: 7, name: 'Mlijeko', bestStore: 'Prodavnica 2', price: 2.45, category: 'namirnice' },
    { id: 8, name: 'Jaja', bestStore: 'Prodavnica 1', price: 3.1, category: 'namirnice' },
    { id: 9, name: 'Keks', bestStore: 'Prodavnica 4', price: 1.8, category: 'namirnice' },
    { id: 10, name: 'Hljeb', bestStore: 'Prodavnica 3', price: 1.4, category: 'namirnice' },
    { id: 11, name: 'Brašno', bestStore: 'Prodavnica 5', price: 2.2, category: 'namirnice' },
    { id: 12, name: 'Riža', bestStore: 'Prodavnica 2', price: 3.5, category: 'namirnice' },
  ];

  electronicsProducts: Product[] = [
    { id: 13, name: 'Miš', bestStore: 'Prodavnica 5', price: 19.9, category: 'elektronika' },
    { id: 14, name: 'Tastatura', bestStore: 'Prodavnica 2', price: 39.9, category: 'elektronika' },
    { id: 15, name: 'Punjač', bestStore: 'Prodavnica 1', price: 24.5, category: 'elektronika' },
    { id: 16, name: 'USB kabl', bestStore: 'Prodavnica 4', price: 9.9, category: 'elektronika' },
  ];

  drugstoreProducts: Product[] = [
    { id: 17, name: 'Šampon', bestStore: 'Prodavnica 3', price: 6.8, category: 'drogerija' },
    { id: 18, name: 'Pasta za zube', bestStore: 'Prodavnica 2', price: 4.2, category: 'drogerija' },
    { id: 19, name: 'Sapun', bestStore: 'Prodavnica 5', price: 2.1, category: 'drogerija' },
    { id: 20, name: 'Detergent', bestStore: 'Prodavnica 1', price: 12.5, category: 'drogerija' },
  ];

  dealsProducts: Product[] = [
    { id: 21, name: 'Kafa', bestStore: 'Prodavnica 3', price: 7.9, category: 'akcije' },
    { id: 22, name: 'Čokolada', bestStore: 'Prodavnica 4', price: 2.3, category: 'akcije' },
    { id: 23, name: 'Detergent', bestStore: 'Prodavnica 1', price: 10.9, category: 'akcije' },
    { id: 24, name: 'USB kabl', bestStore: 'Prodavnica 4', price: 7.9, category: 'akcije' },
  ];

  favoritesCount = computed(() => this.favoritesService.favorites().length);
  isAuthenticated = computed(() => this.currentUser.isAuthenticated());

  get profileLink(): string {
    return this.isAuthenticated() ? '/client/profile' : '/auth/login';
  }

  get userLabel(): string {
    const email = this.currentUser.snapshot?.email;
    if (!email) return 'Gost';
    return email.split('@')[0] ?? 'Kupac';
  }

  setCategory(cat: CategoryKey): void {
    this.activeCategory = cat;
    localStorage.setItem(this.storageKey, cat);
    this.clearCompare();
    this.updateActiveContent();

    if (cat === 'prodavnice') this.scrollTo('stores');
    else this.scrollTo('popular');
  }

  scrollTo(id: string): void {
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }

  toggleFavorite(productName: string, price?: number | null): void {
    this.favoritesService.toggle(productName, {
      price: price ?? null,
      imageUrl: "assets/cart-icon.png"
    });
  }

  isFavorite(productName: string): boolean {
    return this.favoritesService.isFavorite(productName);
  }

  onSearch(): void {
    this.updateActiveContent();
    this.scrollTo('popular');
  }

  onSearchTermChange(value: string): void {
    this.searchTerm = value;
    this.updateActiveContent();
  }

  onCompare(p: Product): void {
    if (this.compareSelection.some(item => item.id === p.id)) {
      return;
    }

    if (this.compareSelection.length === 0) {
      this.compareSelection = [p];
      this.comparePair = null;
      return;
    }

    const left = this.compareSelection[0];
    this.comparePair = { left, right: p };
    this.compareSelection = [left, p];
  }

  clearCompare(): void {
    this.compareSelection = [];
    this.comparePair = null;
  }

  isMarkedForCompare(productId: number): boolean {
    return this.compareSelection.some(item => item.id === productId);
  }

  get compareSavings(): number {
    if (!this.comparePair) {
      return 0;
    }
    return Math.abs(this.comparePair.left.price - this.comparePair.right.price);
  }

  trackByProductId(_: number, item: Product) {
    return item.id;
  }

  trackByStore(_: number, store: Store) {
    return store.name;
  }

  private updateActiveContent(): void {
    const normalizedSearchTerm = this.searchTerm.trim().toLowerCase();
    const searchFilter = (product: Product): boolean =>
      !normalizedSearchTerm ||
      product.name.toLowerCase().includes(normalizedSearchTerm) ||
      product.bestStore.toLowerCase().includes(normalizedSearchTerm);

    switch (this.activeCategory) {
      case 'popularno':
        this.visibleProducts = this.popularProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
      case 'namirnice':
        this.visibleProducts = this.groceriesProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
      case 'elektronika':
        this.visibleProducts = this.electronicsProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
      case 'drogerija':
        this.visibleProducts = this.drugstoreProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
      case 'akcije':
        this.visibleProducts = this.dealsProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
      case 'prodavnice':
        this.visibleProducts = [];
        this.visibleStores = this.stores;
        break;
      default:
        this.visibleProducts = this.popularProducts;
        this.visibleStores = [];
    }
  }

  private isCategoryKey(value: string): value is CategoryKey {
    return this.categoryTabs.some(tab => tab.key === value);
  }
}

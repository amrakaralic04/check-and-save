import { Component, computed, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { FavoritesService } from '../../../shared/services/favorites.services';
import { CurrentUserService } from '../../../../core/services/auth/current-user.service';
import { UsersApiService } from '../../../../api-services/users/users-api.services';
import { UserProfileDto } from '../../../../api-services/users/users-api.model';
import { ProductImageCarouselComponent } from '../../../public/product-image-carousel/product-image-carousel.component';
import { ProductsApiService } from '../../../../api-services/products/products-api.service';
import { ListProductsQuery } from '../../../../api-services/products/products-api.models';

type CategoryKey =
  | 'popularno'
  | 'namirnice'
  | 'elektronika'
  | 'drogerija'
  | 'prodavnice'
  | 'akcije';

type Product = {
  id: number;
  name: string;
  bestStore: string;
  price: number;
  category: string;
  images: string[];
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
  imports: [CommonModule, RouterLink, FormsModule, MatIconModule, ProductImageCarouselComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  private favoritesService = inject(FavoritesService);
  private currentUser = inject(CurrentUserService);
  private usersApi = inject(UsersApiService);
  private productsApi = inject(ProductsApiService);

  activeCategory: CategoryKey = 'popularno';
  private readonly storageKey = 'client-home-active-category';

  searchTerm = '';
  visibleProducts: Product[] = [];
  visibleStores: Store[] = [];
  compareSelection: Product[] = [];
  comparePair: { left: Product; right: Product } | null = null;
  isLoadingProducts = false;

  userProfile: UserProfileDto | null = null;

  private productCache: Map<string, Product[]> = new Map();

  categoryTabs: CategoryTab[] = [
    { key: 'popularno', label: 'Popularno', subtitle: 'Top ponude danas' },
    { key: 'namirnice', label: 'Namirnice', subtitle: 'Svježe i osnovno' },
    { key: 'elektronika', label: 'Elektronika', subtitle: 'Uređaji i oprema' },
    { key: 'drogerija', label: 'Drogerija', subtitle: 'Njega i higijena' },
    { key: 'prodavnice', label: 'Prodavnice', subtitle: 'Lokacije i radno vrijeme' },
    { key: 'akcije', label: 'Akcije', subtitle: 'Aktuelni popusti' },
  ];

  stores: Store[] = [
    { name: 'Prodavnica 1', city: 'Mostar', hours: '07:00 - 21:00' },
    { name: 'Prodavnica 2', city: 'Mostar', hours: '08:00 - 22:00' },
    { name: 'Prodavnica 3', city: 'Mostar', hours: '07:30 - 20:30' },
    { name: 'Prodavnica 4', city: 'Mostar', hours: '08:00 - 21:30' },
    { name: 'Prodavnica 5', city: 'Mostar', hours: '09:00 - 20:00' },
  ];

  favoritesCount = computed(() => this.favoritesService.favorites().length);
  isAuthenticated = computed(() => this.currentUser.isAuthenticated());

  get profileLink(): string {
    return this.isAuthenticated() ? '/client/profile' : '/auth/login';
  }

  get userLabel(): string {
    const firstName = this.userProfile?.firstName?.trim() ?? '';
    const lastName = this.userProfile?.lastName?.trim() ?? '';
    const fullName = `${firstName} ${lastName}`.trim();
    if (fullName) return fullName;
    const email = this.currentUser.snapshot?.email;
    if (!email) return 'Gost';
    return email.split('@')[0] ?? 'Kupac';
  }

  get userInitials(): string {
    const firstInitial = this.userProfile?.firstName?.trim().charAt(0) ?? '';
    const lastInitial = this.userProfile?.lastName?.trim().charAt(0) ?? '';
    const initials = `${firstInitial}${lastInitial}`.trim().toUpperCase();
    if (initials) return initials;
    return this.userLabel.charAt(0).toUpperCase();
  }

  ngOnInit(): void {
    const storedCategory = localStorage.getItem(this.storageKey);
    if (storedCategory && this.isCategoryKey(storedCategory)) {
      this.activeCategory = storedCategory;
    }

    if (this.currentUser.isAuthenticated()) {
      this.usersApi.getMe().subscribe({
        next: (profile) => { this.userProfile = profile; },
        error: () => { this.userProfile = null; },
      });
    }

    this.loadProductsFromApi();
  }

  private loadProductsFromApi(): void {
    this.isLoadingProducts = true;

    const query = new ListProductsQuery();
    query.paging = { page: 1, pageSize: 50 };

    this.productsApi.list(query).subscribe({
      next: (response) => {
        const mapped: Product[] = response.items.map((p: any) => ({
          id: p.id,
          name: p.name,
          bestStore: p.storeLabel ?? 'Nepoznato',
          price: p.lowestPrice ?? 0,
          category: 'popularno',
          images: p.imageURL ? [p.imageURL] : [],
        }));

        this.productCache.set('popularno', mapped);
        this.isLoadingProducts = false;
        this.updateActiveContent();
      },
      error: () => {
        this.isLoadingProducts = false;
        this.updateActiveContent();
      }
    });
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

  toggleFavorite(productName: string, price?: number | null, imageUrl?: string): void {
    this.favoritesService.toggle(productName, {
      price: price ?? null,
      imageUrl: imageUrl ?? 'assets/cart-icon.png',
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
    if (this.compareSelection.some((item) => item.id === p.id)) return;

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
    return this.compareSelection.some((item) => item.id === productId);
  }

  get compareSavings(): number {
    if (!this.comparePair) return 0;
    return Math.abs(this.comparePair.left.price - this.comparePair.right.price);
  }

  trackByProductId(_: number, item: Product): number {
    return item.id;
  }

  trackByStore(_: number, store: Store): string {
    return store.name;
  }

  private updateActiveContent(): void {
    const normalizedSearchTerm = this.searchTerm.trim().toLowerCase();
    const allProducts = this.productCache.get('popularno') ?? [];

    const searchFilter = (product: Product): boolean =>
      !normalizedSearchTerm ||
      product.name.toLowerCase().includes(normalizedSearchTerm) ||
      product.bestStore.toLowerCase().includes(normalizedSearchTerm);

    switch (this.activeCategory) {
      case 'prodavnice':
        this.visibleProducts = [];
        this.visibleStores = this.stores;
        break;
      default:
        this.visibleProducts = allProducts.filter(searchFilter);
        this.visibleStores = [];
        break;
    }
  }

  private isCategoryKey(value: string): value is CategoryKey {
    return this.categoryTabs.some((tab) => tab.key === value);
  }
}

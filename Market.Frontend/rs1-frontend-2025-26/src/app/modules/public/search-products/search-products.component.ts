import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin, of, Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToasterService } from '../../../core/services/toaster.service';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { FavoritesService } from '../../shared/services/favorites.services';
import { ProductsApiService } from '../../../api-services/products/products-api.service';
import {
  ListProductsQuery,
  ListProductsQueryDto
} from '../../../api-services/products/products-api.models';
import { CategoriesApiService } from '../../../api-services/category/category-api.service';
import {
  ListCategoriesQueryResponse
} from '../../../api-services/category/category-api.model';
import { allItemsPaging } from '../../../core/models/paging/paging-utils';
import { ComparisonService } from '../services/comparison.service';

interface GlobalAction {
  icon: string;
  title: string;
  subtitle: string;
}

interface ProductCard {
  id: number;
  name: string;
  category: string;
  storeLabel: string;
  unit?: string;
  note?: string;
  price?: number | null;
  oldPrice?: number;
  badge?: string;
  imageBg: string;
  images: string[];
}

@Component({
  selector: 'app-search-products',
  standalone: false,
  templateUrl: './search-products.component.html',
  styleUrl: './search-products.component.scss',
})
export class SearchProductsComponent implements OnInit, OnDestroy {
  private currentUser = inject(CurrentUserService);
  private favoritesService = inject(FavoritesService);
  private router = inject(Router);
  private toaster = inject(ToasterService);
  private productsApi = inject(ProductsApiService);
  private categoriesApi = inject(CategoriesApiService);

  private comparisonService = inject(ComparisonService);
  private comparisonSubscription?: Subscription;
  selectedComparisonIds: number[] = [];
  query = '';
  activeTag = 'Popularno';
  resultsTitle = 'Najnovije preporuke';
  resultsSubtitle = 'Prikazujemo odabrane proizvode iz više prodavnica.';
  resultsCount = 0;

  private comparisonBaseProductId: number | null = null;

  private categoryById = new Map<number, string>();

  quickTags = ['Popularno', 'Akcije', 'Bez laktoze', 'Bio/eko', 'Higijena', 'Pića'];

  products: ProductCard[] = [];
  filteredProducts: ProductCard[] = [];

  globalActions: GlobalAction[] = [
    { icon: 'tune', title: 'Filteri', subtitle: 'Dodaj detaljne filtere po cijeni i kategoriji.' },
    { icon: 'swap_vert', title: 'Sortiranje', subtitle: 'Posloži rezultate po najnižoj cijeni.' },
    { icon: 'storefront', title: 'Prodavnice', subtitle: 'Pregledaj dostupne prodavnice u blizini.' },
    { icon: 'favorite_border', title: 'Omiljeno', subtitle: 'Sačuvaj proizvode za kasnije upoređivanje.' },
  ];

  ngOnInit(): void {
    this.selectedComparisonIds = this.comparisonService.selectedProductIds;
    this.comparisonSubscription = this.comparisonService.selectedProductIds$.subscribe((ids) => {
      this.selectedComparisonIds = ids;
    });
    this.loadProducts();
  }

  ngOnDestroy(): void {
    this.comparisonSubscription?.unsubscribe();
  }

  setActiveTag(tag: string): void {
    this.activeTag = tag;
    this.updateResults(this.query);
  }

  onQueryChange(value: string): void {
    this.query = value;
    this.updateResults(value);
  }

  onSearch(): void {
    this.updateResults(this.query, true);
  }

  onCardClick(productId: number, event: Event): void {
    if (this.isComparisonTrigger(event)) {
      this.toggleCompare(productId, event);
      return;
    }

    this.router.navigate(['/product', productId]);
  }

  toggleCompare(productId: number, event?: Event): void {
    event?.stopPropagation();
    this.comparisonService.toggleProduct(productId);

    const isSelected = this.isSelectedForComparison(productId);
    this.toaster.info(
      isSelected ? 'Proizvod je dodan za poređenje.' : 'Proizvod je uklonjen iz poređenja.'
    );
  }

  isSelectedForComparison(productId: number): boolean {
    return this.selectedComparisonIds.includes(productId);
  }

  clearComparison(event?: Event): void {
    event?.stopPropagation();
    this.comparisonService.clear();
  }

  onAddToFavorites(product: ProductCard, event?: Event): void {
    event?.stopPropagation();

    if (!this.currentUser.isAuthenticated()) {
      this.toaster.warning('Prijavite se da biste dodali omiljeni proizvod.');
      this.router.navigate(['/auth/login']);
      return;
    }

    this.favoritesService.toggle(product.name, {
      publicId: product.id.toString(),
      price: product.price ?? null,
      imageUrl: product.images[0] ?? 'assets/cart-icon.png'
    });

    const isFavorite = this.favoritesService.isFavorite(product.name);
    this.toaster.success(
      isFavorite ? 'Proizvod je dodan u omiljene.' : 'Proizvod je uklonjen iz omiljenih.'
    );
  }

  private isComparisonTrigger(event: Event): boolean {
    if (!(event instanceof MouseEvent || event instanceof KeyboardEvent)) return false;
    return event.ctrlKey || event.metaKey;
  }

  private updateResults(value: string, fromSubmit = false): void {
    const term = value.trim().toLowerCase();
    const activeTag = this.activeTag;

    this.filteredProducts = this.products.filter((product) => {
      const matchesTerm = !term || product.name.toLowerCase().includes(term);
      const matchesTag =
        activeTag === 'Popularno' ||
        product.category === activeTag ||
        product.badge === activeTag;
      return matchesTerm && matchesTag;
    });

    this.resultsCount = this.filteredProducts.length;

    if (!term) {
      if (this.resultsCount === 0 && activeTag !== 'Popularno') {
        this.resultsTitle = `Nema proizvoda u kategoriji ${activeTag}`;
        this.resultsSubtitle = 'Pokušajte drugi filter ili uklonite brzi tag.';
        return;
      }
      this.resultsTitle = activeTag === 'Popularno'
        ? 'Najnovije preporuke'
        : `Istaknuto za ${activeTag}`;
      this.resultsSubtitle = 'Prikazujemo odabrane proizvode iz više prodavnica.';
      return;
    }

    this.resultsTitle = fromSubmit
      ? `Rezultati pretrage za "${value.trim()}"`
      : `Pretraga u toku: "${value.trim()}"`;
    this.resultsSubtitle =
      this.resultsCount === 0
        ? 'Nema proizvoda koji odgovaraju upitu. Pokušajte drugi pojam.'
        : `Pronađeno ${this.resultsCount} proizvoda u prodavnicama.`;
  }

  private loadProducts(): void {
    const request = new ListProductsQuery();

    forkJoin({
      products: this.productsApi.list(request),
      categories: this.categoriesApi.list({ paging: allItemsPaging }).pipe(
        catchError((error) => {
          console.error('Load categories error:', error);
          this.toaster.warning('Kategorije nisu dostupne, prikazujemo osnovne podatke.');
          return of({
            items: [],
            pageSize: 0,
            currentPage: 1,
            includedTotal: false,
            totalItems: 0,
            totalPages: 0
          } as ListCategoriesQueryResponse);
        })
      )
    }).subscribe({
      next: ({ products, categories }) => {
        this.categoryById = new Map(categories.items.map((c) => [c.id, c.name]));
        this.products = products.items.map((p, i) => this.mapProduct(p, i));
        this.filteredProducts = [...this.products];
        this.resultsCount = this.filteredProducts.length;
        this.updateResults(this.query);
      },
      error: (error) => {
        console.error('Load products error:', error);
        this.toaster.error('Greška pri učitavanju proizvoda.');
        this.products = [];
        this.filteredProducts = [];
        this.resultsCount = 0;
      }
    });
  }

  private mapProduct(product: ListProductsQueryDto, index: number): ProductCard {
    return {
      id: product.id,
      name: product.name,
      category: this.categoryById.get(product.categoryEntityId) ?? `#${product.categoryEntityId}`,
      storeLabel: product.storeLabel ?? 'Nepoznata prodavnica',
      note: 'Najniža cijena trenutno dostupna.',
      price: product.lowestPrice ?? null,
      imageBg: this.resolveImageBg(index),
      images: [],
    };
  }

  private resolveImageBg(index: number): string {
    const gradients = [
      'linear-gradient(135deg, #fef6e7, #f8d9a0)',
      'linear-gradient(135deg, #f4faff, #cfe8ff)',
      'linear-gradient(135deg, #fff8e4, #f9d783)',
      'linear-gradient(135deg, #f0f7ff, #c7d8ff)',
      'linear-gradient(135deg, #e7fff9, #b8f2e8)',
      'linear-gradient(135deg, #fff0f4, #ffd3e0)',
      'linear-gradient(135deg, #f1fff3, #c7f0cd)'
    ];
    return gradients[index % gradients.length];
  }
}

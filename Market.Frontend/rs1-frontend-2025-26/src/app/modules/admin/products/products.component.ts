import { Component, inject, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import {
  ListProductsQuery,
  ListProductsQueryDto
} from '../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../api-services/products/products-api.service';
import {
  ListCategoriesQueryDto
} from '../../../api-services/category/category-api.model';
import { CategoriesApiService } from '../../../api-services/category/category-api.service';
import { BrandsApiService } from '../../../api-services/brand/brand-api.service';
import { ListBrandsQueryDto } from '../../../api-services/brand/brand-api.model';
import { ToasterService } from '../../../core/services/toaster.service';
import { allItemsPaging } from '../../../core/models/paging/paging-utils';
import { StoresApiService } from '../../../api-services/stores/stores-api.service';
import { ListStoresQueryDto } from '../../../api-services/stores/stores-api.models';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { DialogButton } from '../../shared/models/dialog-config.model';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent
  extends BaseListPagedComponent<ListProductsQueryDto, ListProductsQuery>
  implements OnInit {

  private productsApi = inject(ProductsApiService);
  private categoriesApi = inject(CategoriesApiService);
  private brandsApi = inject(BrandsApiService);
  private storesApi = inject(StoresApiService);
  private toaster = inject(ToasterService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private dialogHelper = inject(DialogHelperService);

  displayedColumns: string[] = [
    'name',
    'category',
    'brand',
    'store',
    'dateAdded',
    'actions'
  ];

  categories: ListCategoriesQueryDto[] = [];
  brands: ListBrandsQueryDto[] = [];
  stores: ListStoresQueryDto[] = [];

  private categoryById = new Map<number, string>();
  private brandById = new Map<number, string>();
  private storeById = new Map<number, string>();

  constructor() {
    super();
    this.request = new ListProductsQuery();
    // Postavi default paging vrijednosti
    this.request.paging = {
      page: 1,
      pageSize: 10
    };
  }

  ngOnInit(): void {
    console.log('🚀 ProductsComponent ngOnInit');
    this.loadFilters();
    this.initList();

    // Auto-refresh nakon dodavanja/izmjene
    this.route.queryParams.subscribe(params => {
      if (params['refresh']) {
        console.log('🔄 Auto-refresh triggered');
        setTimeout(() => {
          this.loadPagedData();
        }, 100);
      }
    });
  }

  protected loadPagedData(): void {
    console.log('📊 loadPagedData called');
    console.log('Request object:', JSON.stringify(this.request, null, 2));

    this.startLoading();

    this.productsApi.list(this.request).subscribe({
      next: (response) => {
        console.log('✅ Products loaded successfully:', {
          total: response.totalItems,
          itemsCount: response.items?.length || 0,
          pageSize: response.pageSize
        });

        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        console.error('❌ Load products FAILED');
        console.error('Status:', err.status);
        console.error('Status Text:', err.statusText);
        console.error('Error:', err.error);
        console.error('Message:', err.message);
        console.error('URL:', err.url);
        console.error('Full error:', err);

        this.items = [];
        this.totalItems = 0;
        this.totalPages = 0;
        this.stopLoading();

        // Detaljnija greška
        let errorMessage = 'Greška pri učitavanju proizvoda';

        if (err.status === 400) {
          errorMessage = 'Nevažeći parametri (400 Bad Request)';
          if (err.error?.errors) {
            console.error('Validation errors:', err.error.errors);
            errorMessage += ': ' + JSON.stringify(err.error.errors);
          }
        } else if (err.status === 401) {
          errorMessage = 'Neautorizovani pristup - prijavite se ponovo';
        } else if (err.status === 404) {
          errorMessage = 'API endpoint nije pronađen (404)';
        } else if (err.status === 0) {
          errorMessage = 'Server nije dostupan - provjerite konekciju';
        } else if (err.status >= 500) {
          errorMessage = 'Greška na serveru (' + err.status + ')';
        }

        this.toaster.error(errorMessage);
      }
    });
  }

  onSearch(): void {
    console.log('🔍 Search:', this.request.search);
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  onCreate(): void {
    this.router.navigate(['/admin/products/add']);
  }

  onEdit(product: ListProductsQueryDto): void {
    this.router.navigate(['/admin/products', product.id, 'edit']);
  }

  onDelete(product: ListProductsQueryDto): void {
    this.dialogHelper.product.confirmDelete(product.name).subscribe((result) => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(product);
      }
    });
  }

  clearSearch(): void {
    this.request.search = '';
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  onFilterChange(): void {
    console.log('🔧 Filters changed:', {
      category: this.request.categoryEntityId,
      brand: this.request.brandEntityId,
      store: this.request.storeEntityId
    });
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  resetFilters(): void {
    console.log('♻️ Reset filters');
    this.request.search = '';
    this.request.categoryEntityId = null;
    this.request.brandEntityId = null;
    this.request.storeEntityId = null;
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  private performDelete(product: ListProductsQueryDto): void {
    this.startLoading();

    this.productsApi.delete(product.id).subscribe({
      next: () => {
        console.log('✅ Product deleted:', product.id);
        this.toaster.success('Proizvod je uspješno obrisan');
        this.loadPagedData();
      },
      error: (err) => {
        console.error('❌ Delete failed:', err);
        this.stopLoading();
        this.dialogHelper.product.showDeleteError().subscribe();
      }
    });
  }

  getCategoryName(categoryId: number): string {
    return this.categoryById.get(categoryId) ?? `#${categoryId}`;
  }

  getBrandName(brandId: number): string {
    return this.brandById.get(brandId) ?? `#${brandId}`;
  }

  getStoreName(storeId: number): string {
    return this.storeById.get(storeId) ?? `#${storeId}`;
  }

  private loadFilters(): void {
    console.log('📦 Loading filters...');

    this.categoriesApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => {
        this.categories = response.items;
        this.categoryById = new Map(
          response.items.map((category) => [category.id, category.name])
        );
        console.log('✅ Categories:', this.categories.length);
      },
      error: (err) => {
        console.error('❌ Categories failed:', err);
        this.toaster.error('Greška pri učitavanju kategorija');
      }
    });

    this.brandsApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => {
        this.brands = response.items;
        this.brandById = new Map(
          response.items.map((brand) => [brand.id, brand.name])
        );
        console.log('✅ Brands:', this.brands.length);
      },
      error: (err) => {
        console.error('❌ Brands failed:', err);
        this.toaster.error('Greška pri učitavanju brendova');
      }
    });

    this.storesApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => {
        this.stores = response.items;
        this.storeById = new Map(
          response.items.map((store) => [store.id, store.name])
        );
        console.log('✅ Stores:', this.stores.length);
      },
      error: (err) => {
        console.error('❌ Stores failed:', err);
        this.toaster.error('Greška pri učitavanju prodavnica');
      }
    });
  }
}

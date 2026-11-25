import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ProductsApiService } from '../../../api-services/products/products-api.service';
import { CompareProductDto, CompareStorePriceDto } from '../../../api-services/products/products-api.models';
import { ComparisonService } from '../services/comparison.service';


interface ComparisonRow {
  productId: number;
  productName: string;
  imageUrl: string;
  storeId: number;
  storeName: string;
  branchAddress: string;
  price: number | null;
  dateUpdated: string | null;
}

@Component({
  selector: 'app-product-comparison',
  standalone: false,
  templateUrl: './product-comparison.component.html',
  styleUrl: './product-comparison.component.scss',
})
export class ProductComparisonComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly comparisonService = inject(ComparisonService);
  private readonly productsApi = inject(ProductsApiService);


  protected selectedProductIds: number[] = [];
  protected rows: ComparisonRow[] = [];
  protected isLoading = false;
  protected errorMessage: string | null = null;

  ngOnInit(): void {
    this.selectedProductIds = this.resolveSelectedProductIds();

    if (this.selectedProductIds.length === 0) {
      this.errorMessage = 'Odaberite najmanje jedan proizvod za poređenje.';
      return;
    }

    this.loadComparisonRows();
  }

  protected get cheapestRow(): ComparisonRow | null {
    return this.rows
      .filter((row) => row.price !== null)
      .reduce<ComparisonRow | null>((cheapest, row) => {
        if (!cheapest || (row.price ?? Number.MAX_SAFE_INTEGER) < (cheapest.price ?? Number.MAX_SAFE_INTEGER)) {
          return row;
        }

        return cheapest;
      }, null);
  }

  protected clearComparison(): void {
    this.comparisonService.clear();
    this.router.navigate(['/']);
  }

  private resolveSelectedProductIds(): number[] {
    const serviceIds = this.comparisonService.selectedProductIds;
    if (serviceIds.length > 0) {
      return serviceIds;
    }


    const leftId = Number(this.route.snapshot.paramMap.get('leftId'));
    const rightId = Number(this.route.snapshot.paramMap.get('rightId'));
    const routeIds = [leftId, rightId].filter((id) => Number.isFinite(id) && id > 0);

    if (routeIds.length > 0) {
      this.comparisonService.setSelectedProductIds(routeIds);
    }

    return routeIds;
  }

  private loadComparisonRows(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.productsApi.compare(this.selectedProductIds).subscribe({
      next: (response) => {
        this.rows = response.products
          .flatMap((product) => this.mapComparisonRows(product))
          .sort((left, right) => (left.price ?? Number.MAX_SAFE_INTEGER) - (right.price ?? Number.MAX_SAFE_INTEGER));
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Nije moguće učitati odabrane proizvode za poređenje.';
        this.rows = [];
        this.isLoading = false;
      }
    });
  }

  private mapComparisonRows(product: CompareProductDto): ComparisonRow[] {
    return product.prices.map((price) => this.mapComparisonRow(product, price));
  }
  private mapComparisonRow(product: CompareProductDto, price: CompareStorePriceDto): ComparisonRow {
    return {
      productId: price.productId,
      productName: product.name,
      imageUrl: product.imageURL || 'assets/cart-icon.png',
      storeId: price.storeEntityId,
      storeName: price.storeName || `Prodavnica #${price.storeEntityId}`,
      branchAddress: price.branchAddress,
      price: price.amount ?? null,
      dateUpdated: price.dateUpdated ?? null
    };
  }
}

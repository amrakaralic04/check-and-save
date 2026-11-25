import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import { CreateProductCommand } from '../../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../../api-services/products/products-api.service';
import { ProductFormService } from '../services/product-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { CategoriesApiService } from '../../../../api-services/category/category-api.service';
import { BrandsApiService } from '../../../../api-services/brand/brand-api.service';
import { StoresApiService } from '../../../../api-services/stores/stores-api.service';
import { BranchesApiService } from '../../../../api-services/branches/branches-api.service';
import { ListCategoriesQueryDto } from '../../../../api-services/category/category-api.model';
import { ListBrandsQueryDto } from '../../../../api-services/brand/brand-api.model';
import { ListStoresQueryDto } from '../../../../api-services/stores/stores-api.models';
import { ListBranchesQueryDto } from '../../../../api-services/branches/branches-api.models';
import { allItemsPaging } from '../../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-products-add',
  standalone: false,
  templateUrl: './products-add.component.html',
  styleUrl: './products-add.component.scss',
  providers: [ProductFormService]
})
export class ProductsAddComponent extends BaseFormComponent<any> implements OnInit {

  private api = inject(ProductsApiService);
  private categoriesApi = inject(CategoriesApiService);
  private brandsApi = inject(BrandsApiService);
  private storesApi = inject(StoresApiService);
  private branchesApi = inject(BranchesApiService);
  private formService = inject(ProductFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  categories: ListCategoriesQueryDto[] = [];
  brands: ListBrandsQueryDto[] = [];
  stores: ListStoresQueryDto[] = [];
  branches: ListBranchesQueryDto[] = [];
  filteredBranches: ListBranchesQueryDto[] = [];

  ngOnInit(): void {
    this.initForm(false);
    this.loadFilters();
    this.setupBranchFiltering();
  }

  protected loadData(): void {}

  protected save(): void {
    if (this.form.invalid) {
      Object.keys(this.form.controls).forEach(key => {
        this.form.get(key)?.markAsTouched();
      });
      this.toaster.warning('Molimo popunite sva obavezna polja');
      return;
    }

    if (this.isLoading) return;

    this.startLoading();

    const formValue = this.form.getRawValue();

    const command: CreateProductCommand = {
      ...formValue,
      imageURL: 'placeholder',
      dateAdded: formValue.dateAdded instanceof Date
        ? formValue.dateAdded.toISOString()
        : formValue.dateAdded
    };

    this.api.create(command).subscribe({
      next: (response) => {
        this.stopLoading();
        this.toaster.success('Proizvod dodan! Sada dodajte sliku.');
        // Redirect na edit gdje se uploaduje slika
        this.router.navigate(['/admin/products', response.id, 'edit']);
      },
      error: (err) => {
        this.stopLoading();

        let errorMsg = 'Greška pri dodavanju proizvoda';

        if (err.status === 400 && err.error?.errors) {
          const validationErrors = Object.entries(err.error.errors)
            .map(([field, messages]: [string, any]) => `${field}: ${Array.isArray(messages) ? messages.join(', ') : messages}`)
            .join('; ');
          errorMsg = validationErrors || errorMsg;
        } else if (err.error?.message) {
          errorMsg = err.error.message;
        } else if (err.status === 0) {
          errorMsg = 'Greška u konekciji sa serverom.';
        } else if (err.status === 401) {
          errorMsg = 'Nemate autorizaciju za ovu akciju';
        } else if (err.status >= 500) {
          errorMsg = 'Greška na serveru. Pokušajte ponovo kasnije.';
        }

        this.errorMessage = errorMsg;
        this.toaster.error(errorMsg);
      }
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createProductForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/products']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }

  private loadFilters(): void {
    this.categoriesApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => { this.categories = response.items; },
      error: () => { this.toaster.error('Greška pri učitavanju kategorija'); }
    });

    this.brandsApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => { this.brands = response.items; },
      error: () => { this.toaster.error('Greška pri učitavanju brendova'); }
    });

    this.storesApi.list({ paging: allItemsPaging, onlyActive: true }).subscribe({
      next: (response) => { this.stores = response.items; },
      error: () => { this.toaster.error('Greška pri učitavanju prodavnica'); }
    });
  }

  private setupBranchFiltering(): void {
    this.form.get('storeEntityId')?.valueChanges.subscribe((storeId) => {
      this.loadBranches(storeId ?? null);
    });
  }

  private loadBranches(storeId: number | null): void {
    if (!storeId) {
      this.branches = [];
      this.filteredBranches = [];
      this.form.get('branchEntityId')?.setValue(null);
      return;
    }

    this.branchesApi.list({ paging: allItemsPaging, onlyActive: true, storeEntityId: storeId }).subscribe({
      next: (response) => {
        this.branches = response.items;
        this.filteredBranches = response.items;

        const selectedBranchId = this.form.get('branchEntityId')?.value;
        if (selectedBranchId) {
          const stillValid = this.filteredBranches.some(b => b.id === selectedBranchId);
          if (!stillValid) this.form.get('branchEntityId')?.setValue(null);
        }
      },
      error: () => { this.toaster.error('Greška pri učitavanju poslovnica'); }
    });
  }
}

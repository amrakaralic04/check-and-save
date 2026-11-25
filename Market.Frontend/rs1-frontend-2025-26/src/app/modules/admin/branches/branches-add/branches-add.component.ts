import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  CreateBranchCommand,
  GetBranchByIdQueryDto
} from '../../../../api-services/branches/branches-api.models';
import { BranchesApiService } from '../../../../api-services/branches/branches-api.service';
import { BranchFormService } from '../services/branch-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { StoresApiService } from '../../../../api-services/stores/stores-api.service';
import { CitiesApiService } from '../../../../api-services/cities/cities-api.service';
import { ListStoresQueryDto } from '../../../../api-services/stores/stores-api.models';
import { ListCitiesQueryDto } from '../../../../api-services/cities/cities-api.models';
import { largePaging } from '../../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-branches-add',
  standalone: false,
  templateUrl: './branches-add.component.html',
  styleUrl: './branches-add.component.scss',
  providers: [BranchFormService]
})
export class BranchesAddComponent
  extends BaseFormComponent<GetBranchByIdQueryDto>
  implements OnInit {

  private api = inject(BranchesApiService);
  private storesApi = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private formService = inject(BranchFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  stores: ListStoresQueryDto[] = [];
  cities: ListCitiesQueryDto[] = [];

  ngOnInit(): void {
    this.initForm(false);
    this.loadStores();
    this.loadCities();
  }

  protected loadData(): void {
    // Not needed in add mode.
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: CreateBranchCommand = {
      storeEntityId: this.form.value.storeEntityId,
      cityEntityId: this.form.value.cityEntityId,
      address: this.form.value.address,
      contact: this.form.value.contact,
      email: this.form.value.email
    };

    this.api.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.router.navigate(['/admin/branches'], {
          state: { successMessage: 'Poslovnica je uspješno dodana' }
        });
      },
      error: (err) => {
        this.stopLoading('Greška pri dodavanju poslovnice');
        this.toaster.error('Greška pri dodavanju poslovnice');
        console.error('Create branch error:', err);
      }
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createBranchForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/branches']);
  }

  override onSubmit(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid) {
      this.errorMessage = 'Molimo popunite sva obavezna polja.';
      this.toaster.error('Molimo popunite sva obavezna polja.');
      return;
    }

    this.errorMessage = null;
    this.save();
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }

  private loadStores(): void {
    this.storesApi.list({ paging: largePaging }).subscribe({
      next: (response) => {
        this.stores = response.items;
      },
      error: (err) => {
        this.toaster.error('Greška pri učitavanju prodavnica');
        console.error('Load stores error:', err);
      }
    });
  }

  private loadCities(): void {
    this.citiesApi.list({ paging: largePaging }).subscribe({
      next: (response) => {
        this.cities = response.items;
      },
      error: (err) => {
        this.toaster.error('Greška pri učitavanju gradova');
        console.error('Load cities error:', err);
      }
    });
  }
}

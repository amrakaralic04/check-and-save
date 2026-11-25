import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  CreateStoreCommand,
  GetStoreByIdQueryDto
} from '../../../../api-services/stores/stores-api.models';
import { StoresApiService } from '../../../../api-services/stores/stores-api.service';
import { StoreFormService } from '../services/store-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { CitiesApiService } from '../../../../api-services/cities/cities-api.service';
import { ListCitiesQueryDto } from '../../../../api-services/cities/cities-api.models';
import { largePaging } from '../../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-stores-add',
  standalone: false,
  templateUrl: './stores-add.component.html',
  styleUrl: './stores-add.component.scss',
  providers: [StoreFormService]
})
export class StoresAddComponent
  extends BaseFormComponent<GetStoreByIdQueryDto>
  implements OnInit {

  private api = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private formService = inject(StoreFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  cities: ListCitiesQueryDto[] = [];

  ngOnInit(): void {
    this.initForm(false);
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

    const command: CreateStoreCommand = {
      name: this.form.value.name,
      contact: this.form.value.contact,
      email: this.form.value.email,
      cityEntityId: this.form.value.cityEntityId
    };

    this.api.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Store created successfully');
        this.router.navigate(['/admin/stores']);
      },
      error: (err) => {
        this.stopLoading('Failed to create store');
        console.error('Create store error:', err);
      }
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createStoreForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/stores']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }

  private loadCities(): void {
    this.citiesApi.list({ paging: largePaging }).subscribe({
      next: (response) => {
        this.cities = response.items;
      },
      error: (err) => {
        this.toaster.error('Failed to load cities');
        console.error('Load cities error:', err);
      }
    });
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  GetStoreByIdQueryDto,
  UpdateStoreCommand
} from '../../../../api-services/stores/stores-api.models';
import { StoresApiService } from '../../../../api-services/stores/stores-api.service';
import { StoreFormService } from '../services/store-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { CitiesApiService } from '../../../../api-services/cities/cities-api.service';
import { ListCitiesQueryDto } from '../../../../api-services/cities/cities-api.models';
import { largePaging } from '../../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-stores-edit',
  standalone: false,
  templateUrl: './stores-edit.component.html',
  styleUrl: './stores-edit.component.scss',
  providers: [StoreFormService]
})
export class StoresEditComponent
  extends BaseFormComponent<GetStoreByIdQueryDto>
  implements OnInit {

  private api = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private formService = inject(StoreFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  storeId!: number;
  cities: ListCitiesQueryDto[] = [];

  ngOnInit(): void {
    this.storeId = Number(this.route.snapshot.params['id']);
    this.initForm(true);
  }

  protected loadData(): void {
    this.startLoading();

    forkJoin({
      store: this.api.getById(this.storeId),
      cities: this.citiesApi.list({ paging: largePaging })
    }).subscribe({
      next: ({ store, cities }) => {
        this.model = store;
        this.cities = cities.items;
        this.form = this.formService.createStoreForm(store);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Failed to load store');
        this.toaster.error('Store not found');
        console.error('Load store error:', err);
        this.router.navigate(['/admin/stores']);
      }
    });
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: UpdateStoreCommand = {
      name: this.form.value.name,
      contact: this.form.value.contact,
      email: this.form.value.email,
      cityEntityId: this.form.value.cityEntityId,
      isActive: this.form.value.isActive
    };

    this.api.update(this.storeId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Store updated successfully');
        this.router.navigate(['/admin/stores']);
      },
      error: (err) => {
        this.stopLoading('Failed to update store');
        console.error('Update store error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/admin/stores']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  GetBranchByIdQueryDto,
  UpdateBranchCommand
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
  selector: 'app-branches-edit',
  standalone: false,
  templateUrl: './branches-edit.component.html',
  styleUrl: './branches-edit.component.scss',
  providers: [BranchFormService]
})
export class BranchesEditComponent
  extends BaseFormComponent<GetBranchByIdQueryDto>
  implements OnInit {

  private api = inject(BranchesApiService);
  private storesApi = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private formService = inject(BranchFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  branchId!: number;

  stores: ListStoresQueryDto[] = [];
  cities: ListCitiesQueryDto[] = [];

  ngOnInit(): void {
    this.branchId = Number(this.route.snapshot.params['id']);
    this.initForm(true);
  }

  protected loadData(): void {
    this.startLoading();

    forkJoin({
      branch: this.api.getById(this.branchId),
      stores: this.storesApi.list({ paging: largePaging }),
      cities: this.citiesApi.list({ paging: largePaging })
    }).subscribe({
      next: ({ branch, stores, cities }) => {
        this.model = branch;
        this.stores = stores.items;
        this.cities = cities.items;
        this.form = this.formService.createBranchForm(branch);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Greška pri učitavanju poslovnice');
        this.toaster.error('Poslovnica nije pronađena');
        console.error('Load branch error:', err);
        this.router.navigate(['/admin/branches']);
      }
    });
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: UpdateBranchCommand = {
      storeEntityId: this.form.value.storeEntityId,
      cityEntityId: this.form.value.cityEntityId,
      address: this.form.value.address,
      contact: this.form.value.contact,
      email: this.form.value.email,
      isActive: this.form.value.isActive
    };

    this.api.update(this.branchId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.router.navigate(['/admin/branches'], {
          state: { successMessage: 'Poslovnica je uspješno ažurirana' }
        });
      },
      error: (err) => {
        this.stopLoading('Greška pri ažuriranju poslovnice');
        this.toaster.error('Greška pri ažuriranju poslovnice');
        console.error('Update branch error:', err);
      }
    });
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
}

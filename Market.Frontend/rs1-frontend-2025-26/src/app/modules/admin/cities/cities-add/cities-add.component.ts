import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  CreateCityCommand,
  GetCityByIdQueryDto
} from '../../../../api-services/cities/cities-api.models';
import { CitiesApiService } from '../../../../api-services/cities/cities-api.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { CityFormService } from '../services/city-form.service';

@Component({
  selector: 'app-cities-add',
  standalone: false,
  templateUrl: './cities-add.component.html',
  styleUrl: './cities-add.component.scss',
  providers: [CityFormService]
})
export class CitiesAddComponent
  extends BaseFormComponent<GetCityByIdQueryDto>
  implements OnInit {

  private api = inject(CitiesApiService);
  private formService = inject(CityFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  ngOnInit(): void {
    this.initForm(false);
  }

  protected loadData(): void {
    // Not needed in add mode.
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: CreateCityCommand = {
      name: this.form.value.name,
      postalCode: this.form.value.postalCode
    };

    this.api.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Grad je uspješno dodan');
        this.router.navigate(['/admin/cities']);
      },
      error: (err) => {
        this.stopLoading('Greška pri dodavanju grada');
        console.error('Create city error:', err);
      }
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createCityForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/cities']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

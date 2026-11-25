import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';
import {
  GetCityByIdQueryDto,
  UpdateCityCommand
} from '../../../../api-services/cities/cities-api.models';
import { CitiesApiService } from '../../../../api-services/cities/cities-api.service';
import { CityFormService } from '../services/city-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-cities-edit',
  standalone: false,
  templateUrl: './cities-edit.component.html',
  styleUrl: './cities-edit.component.scss',
  providers: [CityFormService]
})
export class CitiesEditComponent
  extends BaseFormComponent<GetCityByIdQueryDto>
  implements OnInit {

  private api = inject(CitiesApiService);
  private formService = inject(CityFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  cityId!: number;

  ngOnInit(): void {
    this.cityId = Number(this.route.snapshot.params['id']);
    this.initForm(true);
  }

  protected loadData(): void {
    this.startLoading();

    this.api.getById(this.cityId).subscribe({
      next: (city) => {
        this.model = city;
        this.form = this.formService.createCityForm(city);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Greška pri učitavanju grada');
        this.toaster.error('Grad nije pronađen');
        console.error('Load city error:', err);
        this.router.navigate(['/admin/cities']);
      }
    });
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: UpdateCityCommand = {
      name: this.form.value.name,
      postalCode: this.form.value.postalCode
    };

    this.api.update(this.cityId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Grad je uspješno ažuriran');
        this.router.navigate(['/admin/cities']);
      },
      error: (err) => {
        this.stopLoading('Greška pri ažuriranju grada');
        console.error('Update city error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/admin/cities']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

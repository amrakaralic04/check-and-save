// brand-edit.component.ts
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';

import {
  GetBrandByIdQueryDto,
  UpsertBrandCommand
} from '../../../../api-services/brand/brand-api.model';

import { BrandsApiService } from '../../../../api-services/brand/brand-api.service';
import { BrandFormService } from '../services/brand-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-brand-edit',
  standalone: false,
  templateUrl: './brand-edit.component.html',
  styleUrl: './brand-edit.component.scss',
  providers: [BrandFormService]
})
export class BrandEditComponent
  extends BaseFormComponent<GetBrandByIdQueryDto>
  implements OnInit {

  private api = inject(BrandsApiService);
  private formService = inject(BrandFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  brandId!: number;

  ngOnInit(): void {
    this.brandId = Number(this.route.snapshot.params['id']);
    this.initForm(true);
  }

  protected loadData(): void {
    this.startLoading();

    this.api.getById(this.brandId).subscribe({
      next: (brand: GetBrandByIdQueryDto) => {
        this.model = brand;
        this.form = this.formService.createBrandForm(brand);
        this.stopLoading();
      },
      error: (err: any) => {
        this.stopLoading('Greška pri učitavanju brenda');
        this.toaster.error('Brend nije pronađen');
        console.error('Load brand error:', err);
        this.router.navigate(['/admin/brands']);
      }
    });
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const command: UpsertBrandCommand =
      this.form.getRawValue() as UpsertBrandCommand;

    this.api.update(this.brandId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Brend je uspješno ažuriran');
        this.router.navigate(['/admin/brands']);
      },
      error: (err: any) => {
        this.stopLoading('Greška pri ažuriranju brenda');
        console.error('Update brand error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/admin/brands']); //
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

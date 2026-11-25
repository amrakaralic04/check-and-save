import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';

import { UpsertBrandCommand } from '../../../../api-services/brand/brand-api.model';
import { BrandsApiService } from '../../../../api-services/brand/brand-api.service';

import { ToasterService } from '../../../../core/services/toaster.service';
import { BrandFormService } from '../services/brand-form.service';

@Component({
  selector: 'app-brand-add',
  standalone: false,
  templateUrl: './brand-add.component.html',
  styleUrl: './brand-add.component.scss',
  providers: [BrandFormService]
})
export class BrandAddComponent
  extends BaseFormComponent<any>
  implements OnInit {

  private api = inject(BrandsApiService);
  private formService = inject(BrandFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  ngOnInit(): void {
    this.initForm(false);
  }

  protected loadData(): void {
    // ništa – add mode
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const command: UpsertBrandCommand =
      this.form.getRawValue() as UpsertBrandCommand;

    this.api.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Brend je uspješno dodan');
        this.router.navigate(['/admin/brands']);
      },
      error: (err: any) => {
        this.stopLoading('Greška pri dodavanju brenda');
        console.error('Create brand error:', err);
      }
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createBrandForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/brands']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}


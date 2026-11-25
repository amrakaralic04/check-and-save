import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';

import { UpsertCategoryCommand } from '../../../../api-services/category/category-api.model';
import { CategoriesApiService } from '../../../../api-services/category/category-api.service';

import { ToasterService } from '../../../../core/services/toaster.service';
import { CategoryFormService } from '../services/category-form.service';

@Component({
  selector: 'app-category-add',
  standalone: false,
  templateUrl: './category-add.component.html',
  styleUrl: './category-add.component.scss',
  providers: [CategoryFormService],
})
export class CategoryAddComponent
  extends BaseFormComponent<any>
  implements OnInit
{
  private api = inject(CategoriesApiService);
  private formService = inject(CategoryFormService);
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

    const command: UpsertCategoryCommand =
      this.form.getRawValue() as UpsertCategoryCommand;

    this.api.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Kategorija je uspješno dodana');
        this.router.navigate(['/admin/categories']);
      },
      error: (err: any) => {
        this.stopLoading('Greška pri dodavanju kategorije');
        console.error('Create category error:', err);
      },
    });
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createCategoryForm();
  }

  onCancel(): void {
    this.router.navigate(['/admin/categories']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

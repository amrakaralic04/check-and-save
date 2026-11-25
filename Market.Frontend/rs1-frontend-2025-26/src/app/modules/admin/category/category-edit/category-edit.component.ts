import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BaseFormComponent } from '../../../../core/components/base-classes/base-form-component';

import {
  GetCategoryByIdQueryDto,
  UpsertCategoryCommand
} from '../../../../api-services/category/category-api.model';

import { CategoriesApiService } from '../../../../api-services/category/category-api.service';
import { CategoryFormService } from '../services/category-form.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-category-edit',
  standalone: false,
  templateUrl: './category-edit.component.html',
  styleUrl: './category-edit.component.scss',
  providers: [CategoryFormService]
})
export class CategoryEditComponent
  extends BaseFormComponent<GetCategoryByIdQueryDto>
  implements OnInit {

  private api = inject(CategoriesApiService);
  private formService = inject(CategoryFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  categoryId!: number;

  ngOnInit(): void {
    this.categoryId = Number(this.route.snapshot.params['id']);
    this.initForm(true);
  }

  protected loadData(): void {
    this.startLoading();

    this.api.getById(this.categoryId).subscribe({
      next: (category: GetCategoryByIdQueryDto) => {
        this.model = category;
        this.form = this.formService.createCategoryForm(category);
        this.stopLoading();
      },
      error: (err: any) => {
        this.stopLoading('Greška pri učitavanju kategorije');
        this.toaster.error('Kategorija nije pronađena');
        console.error('Load category error:', err);
        this.router.navigate(['/admin/categories']);
      }
    });
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const command: UpsertCategoryCommand =
      this.form.getRawValue() as UpsertCategoryCommand;

    this.api.update(this.categoryId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Kategorija je uspješno ažurirana');
        this.router.navigate(['/admin/categories']);
      },
      error: (err: any) => {
        this.stopLoading('Greška pri ažuriranju kategorije');
        console.error('Update category error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/admin/categories']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }
}

import { Injectable, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetCategoryByIdQueryDto } from '../../../../api-services/category/category-api.model';

/**
 * Service for creating and managing category forms.
 * Provides reusable form creation with validation for Add and Edit components.
 */
@Injectable()
export class CategoryFormService {
  private fb = inject(FormBuilder);

  /**
   * Create a category form with validation.
   * If category data is provided, the form is pre-filled (edit mode).
   */
  createCategoryForm(category?: GetCategoryByIdQueryDto): FormGroup {
    return this.fb.group({
      name: [
        category?.name ?? '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(120),
        ],
      ],
      description: [
        category?.description ?? '',
        [
          Validators.maxLength(500),
        ],
      ],
    });
  }

  /**
   * Get validation error message for a form control.
   */
  getErrorMessage(form: FormGroup, controlName: string): string {
    const control = form.get(controlName);
    if (!control || !control.errors || !control.touched) {
      return '';
    }

    const errors = control.errors;

    if (errors['required']) {
      return 'This field is required';
    }
    if (errors['minlength']) {
      return `Minimum ${errors['minlength'].requiredLength} characters required`;
    }
    if (errors['maxlength']) {
      return `Maximum ${errors['maxlength'].requiredLength} characters allowed`;
    }

    return 'Invalid value';
  }
}

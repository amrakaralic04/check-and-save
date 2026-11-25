import { Injectable, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetCityByIdQueryDto } from '../../../../api-services/cities/cities-api.models';

/**
 * Service for creating and managing city forms.
 * Provides reusable form creation with validation for Add and Edit components.
 */
@Injectable()
export class CityFormService {
  private fb = inject(FormBuilder);

  /**
   * Create a city form with validation.
   * If city data is provided, the form is pre-filled (edit mode).
   */
  createCityForm(city?: GetCityByIdQueryDto): FormGroup {
    return this.fb.group({
      name: [
        city?.name ?? '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(120)
        ]
      ],
      postalCode: [
        city?.postalCode ?? null,
        [
          Validators.required,
          Validators.min(1),
          Validators.max(99999)
        ]
      ]
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
    if (errors['min']) {
      return `Minimum value is ${errors['min'].min}`;
    }
    if (errors['max']) {
      return `Maximum value is ${errors['max'].max}`;
    }

    return 'Invalid value';
  }
}

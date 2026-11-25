import { Injectable, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetStoreByIdQueryDto } from '../../../../api-services/stores/stores-api.models';

/**
 * Service for creating and managing store forms.
 * Provides reusable form creation with validation for Add and Edit components.
 */
@Injectable()
export class StoreFormService {
  private fb = inject(FormBuilder);

  /**
   * Create a store form with validation.
   * If store data is provided, the form is pre-filled (edit mode).
   */
  createStoreForm(store?: GetStoreByIdQueryDto): FormGroup {
    return this.fb.group({
      name: [
        store?.name ?? '',
        [Validators.required, Validators.minLength(2), Validators.maxLength(120)]
      ],
      contact: [
        store?.contact ?? '',
        [Validators.required, Validators.minLength(3), Validators.maxLength(120)]
      ],
      email: [
        store?.email ?? '',
        [Validators.required, Validators.email, Validators.maxLength(120)]
      ],
      cityEntityId: [store?.cityEntityId ?? null, [Validators.required]],
      isActive: [store?.isActive ?? true]
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
    if (errors['email']) {
      return 'Invalid email format';
    }

    return 'Invalid value';
  }
}

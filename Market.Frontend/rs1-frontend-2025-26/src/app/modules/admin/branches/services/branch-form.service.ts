import { Injectable, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetBranchByIdQueryDto } from '../../../../api-services/branches/branches-api.models';

/**
 * Service for creating and managing branch forms.
 * Provides reusable form creation with validation for Add and Edit components.
 */
@Injectable()
export class BranchFormService {
  private fb = inject(FormBuilder);

  /**
   * Create a branch form with validation.
   * If branch data is provided, the form is pre-filled (edit mode).
   */
  createBranchForm(branch?: GetBranchByIdQueryDto): FormGroup {
    return this.fb.group({
      storeEntityId: [branch?.storeEntityId ?? null, [Validators.required]],
      cityEntityId: [branch?.cityEntityId ?? null, [Validators.required]],
      address: [
        branch?.address ?? '',
        [Validators.required, Validators.minLength(3), Validators.maxLength(200)]
      ],
      contact: [
        branch?.contact ?? '',
        [Validators.required, Validators.minLength(3), Validators.maxLength(100)]
      ],
      email: [
        branch?.email ?? '',
        [Validators.required, Validators.email, Validators.maxLength(200)]
      ],
      isActive: [branch?.isActive ?? true]
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

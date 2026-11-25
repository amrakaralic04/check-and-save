import { Injectable, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetProductByIdQueryDto } from '../../../../api-services/products/products-api.models';

@Injectable()
export class ProductFormService {
  private fb = inject(FormBuilder);

  createProductForm(
    product?: GetProductByIdQueryDto,
    includeDateAdded: boolean = true
  ): FormGroup {
    const controls: Record<string, any> = {
      name: [product?.name ?? '', [Validators.required, Validators.maxLength(200)]],
      description: [product?.description ?? '', [Validators.required, Validators.maxLength(2000)]],
      // imageURL nije required u add modu — uploaduje se nakon kreiranja
      imageURL: [product?.imageURL ?? '', [Validators.maxLength(500)]],
      categoryEntityId: [product?.categoryEntityId ?? null, [Validators.required]],
      brandEntityId: [product?.brandEntityId ?? null, [Validators.required]],
      storeEntityId: [product?.storeEntityId ?? null, [Validators.required]],
      branchEntityId: [product?.branchEntityId ?? null, [Validators.required]]
    };

    if (includeDateAdded) {
      controls['dateAdded'] = [
        product?.dateAdded ? new Date(product.dateAdded) : new Date(),
        [Validators.required]
      ];
    }

    return this.fb.group(controls);
  }

  getErrorMessage(form: FormGroup, controlName: string): string {
    const control = form.get(controlName);
    if (!control) return '';
    if (!control.touched && !control.dirty) return '';
    if (!control.errors) return '';

    const errors = control.errors;

    if (errors['required']) return this.getRequiredMessage(controlName);
    if (errors['maxlength']) return `Maksimalno ${errors['maxlength'].requiredLength} karaktera dozvoljeno`;
    if (errors['minlength']) return `Minimalno ${errors['minlength'].requiredLength} karaktera potrebno`;
    if (errors['email']) return 'Unesite validnu email adresu';
    if (errors['pattern']) return 'Format nije validan';
    if (errors['min']) return `Minimalna vrijednost je ${errors['min'].min}`;
    if (errors['max']) return `Maksimalna vrijednost je ${errors['max'].max}`;

    return 'Nevažeća vrijednost';
  }

  private getRequiredMessage(controlName: string): string {
    const messages: Record<string, string> = {
      'name': 'Naziv proizvoda je obavezan',
      'description': 'Opis proizvoda je obavezan',
      'imageURL': 'URL slike je obavezan',
      'categoryEntityId': 'Kategorija je obavezna',
      'brandEntityId': 'Brend je obavezan',
      'storeEntityId': 'Prodavnica je obavezna',
      'branchEntityId': 'Poslovnica je obavezna',
      'dateAdded': 'Datum dodavanja je obavezan'
    };
    return messages[controlName] || 'Ovo polje je obavezno';
  }

  validateForm(form: FormGroup): { valid: boolean; errors: string[] } {
    const errors: string[] = [];
    if (form.invalid) {
      Object.keys(form.controls).forEach(key => {
        const control = form.get(key);
        if (control && control.invalid) {
          const errorMessage = this.getErrorMessage(form, key);
          if (errorMessage) errors.push(errorMessage);
        }
      });
    }
    return { valid: form.valid, errors };
  }

  markAllAsTouched(form: FormGroup): void {
    Object.keys(form.controls).forEach(key => {
      const control = form.get(key);
      control?.markAsTouched();
      control?.markAsDirty();
    });
  }
}

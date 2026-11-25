// import { Component } from '@angular/core';
//
// @Component({
//   selector: 'app-forgot-password',
//   standalone: false,
//   templateUrl: './forgot-password.component.html',
//   styleUrl: './forgot-password.component.scss',
// })
// export class ForgotPasswordComponent {
//
// }
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  standalone: false,
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  form: FormGroup;
  isSubmitting = false;

  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  get email() {
    return this.form.get('email')!;
  }

  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const email = this.email.value as string;

    setTimeout(() => {
      this.isSubmitting = false;

      this.successMessage =
        'Ako postoji račun s ovom email adresom, poslat ćemo ti link za resetovanje lozinke.';
      this.form.reset();
    }, 900);
  }
}

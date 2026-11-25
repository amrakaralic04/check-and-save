
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import { RegisterCommand } from '../../../api-services/auth/auth-api.model';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private authApi = inject(AuthApiService);
  private router = inject(Router);
  form: FormGroup;

  hidePassword = true;
  hideConfirmPassword = true;

  isLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group(
      {
        firstName: ['', [Validators.required, Validators.minLength(2)]],
        lastName: ['', [Validators.required, Validators.minLength(2)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
        gender: ['female', [Validators.required]],
      },
      { validators: [this.passwordsMatchValidator] },
    );
  }

  private passwordsMatchValidator(group: FormGroup) {
    const p = group.get('password')?.value;
    const c = group.get('confirmPassword')?.value;
    return p && c && p !== c ? { passwordsMismatch: true } : null;
  }

  onSubmit(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (this.form.invalid || this.isLoading)  {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;

    const payload: RegisterCommand = {
      firstName: this.form.value.firstName ?? '',
      lastName: this.form.value.lastName ?? '',
      email: this.form.value.email ?? '',
      password: this.form.value.password ?? '',
      gender: (this.form.value.gender ?? 'female') as 'male' | 'female',
    };

    this.authApi.register(payload).subscribe({
      next: () => {
        this.isLoading = false;
        this.form.reset();
        this.hidePassword = true;
        this.hideConfirmPassword = true;
        this.router.navigate(['/auth/login'], {
          queryParams: { registered: 'true' }
        });
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage =
          err?.error?.message || err?.error?.title || 'Registracija nije uspjela. Pokušaj ponovo.';
      },
    });
  }
}

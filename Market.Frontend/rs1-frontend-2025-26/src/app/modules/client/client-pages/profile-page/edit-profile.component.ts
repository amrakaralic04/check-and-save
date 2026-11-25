import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Observable, catchError, finalize, map, of, switchMap, timer } from 'rxjs';
import { UsersApiService } from '../../../../api-services/users/users-api.services';
import { UpdateProfileCommand, UserProfileDto } from '../../../../api-services/users/users-api.model';
import { CurrentUserService } from '../../../../core/services/auth/current-user.service';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss'],
})
export class EditProfileComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly usersApi = inject(UsersApiService);
  private readonly currentUser = inject(CurrentUserService);

  isLoading = true;
  isSaving = false;
  loadError = '';
  saveError = '';
  saveSuccess = '';

  readonly avatarOptions = [
    { level: 2, label: 'Ženski avatar', image: 'assets/avatars/female.avatar.svg' },
    { level: 1, label: 'Muški avatar', image: 'assets/avatars/male.avatar.svg' },
  ];
  private currentEmail = '';
  private userId = '';

  readonly form = this.fb.nonNullable.group({
    firstName: ['', [Validators.required, Validators.maxLength(120)]],
    lastName: ['', [Validators.required, Validators.maxLength(120)]],
    email: this.fb.nonNullable.control('', {
      validators: [Validators.required, Validators.email, Validators.maxLength(200)],
      asyncValidators: [this.emailAvailabilityValidator()],
      updateOn: 'blur',
    }),
    avatarLevel: [2, [Validators.required]],
  });

  ngOnInit(): void {
    this.loadProfile();
  }


  save(): void {
    this.saveError = '';
    this.saveSuccess = '';

    if (this.form.pending) {
      this.saveError = 'Sačekajte da se završi provjera email adrese.';
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.saveError = 'Provjerite unesene podatke prije spremanja.';
      return;
    }

    if (!this.currentUser.isAuthenticated()) {
      this.saveError = 'Sesija je istekla. Prijavite se ponovo.';
      return;
    }

    if (!this.userId) {
      this.saveError = 'Nije moguće pronaći korisnika za ažuriranje.';
      return;
    }

    const value = this.form.getRawValue();

    const payload: UpdateProfileCommand = {
      firstName: value.firstName.trim(),
      lastName: value.lastName.trim(),
      email: value.email.trim(),
      avatarLevel: value.avatarLevel,
    };

    this.isSaving = true;

    this.usersApi
      .updateByPublicId(this.userId, payload)
      .pipe(finalize(() => (this.isSaving = false)))
      .subscribe({
        next: (updatedUser) => {
          const updatedProfile = {
            firstName: updatedUser.firstname,
            lastName: updatedUser.lastname,
            email: updatedUser.email,
            avatarLevel: value.avatarLevel,
          };

          this.form.patchValue(updatedProfile);
          this.currentEmail = updatedProfile.email;
          this.form.markAsPristine();
          this.saveSuccess = 'Profil je uspješno promijenjen i sačuvan.';
          this.scrollToStatusMessage();
        },
        error: (error: unknown) => {
          this.saveError = this.getSaveErrorMessage(error);
          this.scrollToStatusMessage();
        },
      });
  }



  private loadProfile(): void {
    this.usersApi.getMe().subscribe({
      next: (user: UserProfileDto) => {
        this.patchForm(user);
        this.userId = String(user.id);
        this.currentEmail = user.email;
        this.isLoading = false;
      },
      error: () => {
        this.loadError = 'Neuspješno učitavanje profila. Pokušajte ponovo.';
        this.isLoading = false;
      },
    });
  }

  private patchForm(user: UserProfileDto): void {
    this.form.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      avatarLevel: this.normalizeAvatarLevel(user.avatarLevel),
    });
  }
  getSelectedAvatarImage(): string {
    const avatarLevel = this.form.controls.avatarLevel.value;

    return this.avatarOptions.find((option) => option.level === avatarLevel)?.image ?? this.avatarOptions[0].image;
  }

  private normalizeAvatarLevel(avatarLevel?: number): number {
    return avatarLevel === 1 || avatarLevel === 2 ? avatarLevel : 2;
  }


  private getSaveErrorMessage(error: unknown): string {
    if (this.isConflictError(error)) {
      return 'Ovaj email je već zauzet.';
    }

    return 'Neuspješno spremanje profila. Pokušajte ponovo.';
  }

  private isConflictError(error: unknown): boolean {
    return typeof error === 'object' && error !== null && 'status' in error && error.status === 409;
  }

  private scrollToStatusMessage(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  private emailAvailabilityValidator(): AsyncValidatorFn {
    return (control: AbstractControl<string>): Observable<ValidationErrors | null> => {
      const email = control.value?.trim();

      if (!email || control.hasError('email')) {
        return of(null);
      }

      if (email.toLowerCase() === this.currentEmail.toLowerCase()) {
        return of(null);
      }

      return timer(300).pipe(
        switchMap(() => this.usersApi.checkEmailAvailability(email)),
        map((result) => (result.isAvailable ? null : { emailTaken: true })),
        catchError(() => of(null)),
      );
    };
  }
}

import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  EmailAvailabilityDto,
  UpdateProfileCommand,
  UpdateProfileCommandDto,
  UserProfileDto,
} from './users-api.model';

@Injectable({
  providedIn: 'root',
})
export class UsersApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/users`;
  private http = inject(HttpClient);

  getMe(): Observable<UserProfileDto> {
    return this.http.get<UserProfileDto>(`${this.baseUrl}/me`);
  }

  checkEmailAvailability(email: string): Observable<EmailAvailabilityDto> {
    const params = new HttpParams().set('email', email);
    return this.http.get<EmailAvailabilityDto>(`${this.baseUrl}/check-email`, {
      params,
    });
  }

  updateByPublicId(
    publicId: string,
    payload: UpdateProfileCommand,

  ): Observable<UpdateProfileCommandDto> {
    return this.http.put<UpdateProfileCommandDto>(`${this.baseUrl}/${publicId}/public-id`, {
      firstname: payload.firstName,
      lastname: payload.lastName,
      email: payload.email,
      avatarLevel: payload.avatarLevel,
    });
  }
}

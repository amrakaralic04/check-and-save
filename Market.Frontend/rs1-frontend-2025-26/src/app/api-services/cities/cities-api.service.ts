import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';

import {
  CreateCityCommand,
  GetCityByIdQueryDto,
  ListCitiesQuery,
  ListCitiesResponse,
  UpdateCityCommand
} from './cities-api.models';

@Injectable({ providedIn: 'root' })
export class CitiesApiService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/City`;

  /** GET /City?paging.page=1&paging.pageSize=20&search=... */
  list(query: ListCitiesQuery): Observable<ListCitiesResponse> {
    const params = buildHttpParams(query);
    return this.http.get<ListCitiesResponse>(this.baseUrl, { params });
  }

  /** GET /City/{id} */
  getById(id: number): Observable<GetCityByIdQueryDto> {
    return this.http.get<GetCityByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /** POST /City */
  create(command: CreateCityCommand): Observable<{ id: number }> {
    // backend vraća CreatedAtAction(..., new { id }) => JSON { id }
    return this.http.post<{ id: number }>(this.baseUrl, command);
  }

  /** PUT /City/{id} */
  update(id: number, command: UpdateCityCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }

  /** DELETE /City/{id} */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

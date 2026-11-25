import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';
import {
  CreateStoreCommand,
  GetStoreByIdQueryDto,
  ListStoresQuery,
  ListStoresResponse,
  UpdateStoreCommand
} from './stores-api.models';

@Injectable({ providedIn: 'root' })
export class StoresApiService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Store`;

  /** GET /Store?paging.page=1&paging.pageSize=20&search=... */
  list(query: ListStoresQuery): Observable<ListStoresResponse> {
    const params = buildHttpParams(query);
    return this.http.get<ListStoresResponse>(this.baseUrl, { params });
  }

  /** GET /Store/{id} */
  getById(id: number): Observable<GetStoreByIdQueryDto> {
    return this.http.get<GetStoreByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /** POST /Store */
  create(command: CreateStoreCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, command);
  }

  /** PUT /Store/{id} */
  update(id: number, command: UpdateStoreCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }

  /** DELETE /Store/{id} */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

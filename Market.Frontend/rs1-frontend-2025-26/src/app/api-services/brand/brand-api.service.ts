import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListBrandsQuery,
  ListBrandsQueryResponse,
  UpsertBrandCommand,
  GetBrandByIdQueryDto
} from './brand-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root',
})
export class BrandsApiService {
  private readonly baseUrl = `${environment.apiUrl}/Brand`;
  private http = inject(HttpClient);

  list(request?: ListBrandsQuery): Observable<ListBrandsQueryResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;

    return this.http.get<ListBrandsQueryResponse>(this.baseUrl, { params });
  }

  getById(id: number): Observable<GetBrandByIdQueryDto> {
    return this.http.get<GetBrandByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  create(payload: UpsertBrandCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, payload);
  }

  update(id: number, payload: UpsertBrandCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

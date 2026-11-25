import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import {
  CompareProductsQueryDto,
  CreateProductCommand,
  GetProductByIdQueryDto,
  ListProductsQuery,
  ListProductsResponse,
  UpdateProductCommand,
  UploadProductImageDto,
} from './products-api.models';

@Injectable({ providedIn: 'root' })
export class ProductsApiService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Product`;

  /** GET /Product */
  list(query: ListProductsQuery): Observable<ListProductsResponse> {
    let params = new HttpParams();

    const page = query.paging?.page ?? 1;
    const pageSize = query.paging?.pageSize ?? 10;

    params = params.set('paging.page', page.toString());
    params = params.set('paging.pageSize', pageSize.toString());

    if (query.search && query.search.trim() !== '') {
      params = params.set('search', query.search.trim());
    }

    if (query.categoryEntityId !== null && query.categoryEntityId !== undefined) {
      params = params.set('categoryEntityId', query.categoryEntityId.toString());
    }

    if (query.brandEntityId !== null && query.brandEntityId !== undefined) {
      params = params.set('brandEntityId', query.brandEntityId.toString());
    }

    if (query.storeEntityId !== null && query.storeEntityId !== undefined) {
      params = params.set('storeEntityId', query.storeEntityId.toString());
    }

    if (query.branchEntityId !== null && query.branchEntityId !== undefined) {
      params = params.set('branchEntityId', query.branchEntityId.toString());
    }

    return this.http.get<ListProductsResponse>(this.baseUrl, { params });
  }

  /** GET /api/products/compare?ids=... */
  compare(ids: Array<number | string>): Observable<CompareProductsQueryDto> {
    const publicIds = ids
      .map((id) => id.toString().trim())
      .filter((id) => id.length > 0);

    const params = new HttpParams().set('ids', publicIds.join(','));
    return this.http.get<CompareProductsQueryDto>(`${environment.apiUrl}/api/products/compare`, { params });
  }

  /** GET /Product/{id} */
  getById(id: number): Observable<GetProductByIdQueryDto> {
    return this.http.get<GetProductByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /** POST /Product */
  create(command: CreateProductCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, command);
  }

  /** PUT /Product/{id} */
  update(id: number, command: UpdateProductCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }

  /** DELETE /Product/{id} */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  /**
   * POST /Product/{id}/images
   * Uploads, compresses and stores image in Supabase Storage.
   * Returns public URL.
   */
  uploadImage(productId: number, file: File): Observable<UploadProductImageDto> {
    const formData = new FormData();
    formData.append('image', file);
    return this.http.post<UploadProductImageDto>(
      `${this.baseUrl}/${productId}/images`,
      formData
    );
  }
}

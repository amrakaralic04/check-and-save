import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  GetCategoryByIdQueryDto,
  ListCategoriesQueryDto,
  ListCategoriesQueryResponse,
  ListCategoriesQuery,
  UpsertCategoryCommand
} from './category-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root',
})
export class CategoriesApiService {
  private readonly baseUrl = `${environment.apiUrl}/Category`;
  private http = inject(HttpClient);

  /** GET /Category */
  list(request?: ListCategoriesQuery): Observable<ListCategoriesQueryResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;

    return this.http.get<ListCategoriesQueryResponse>(this.baseUrl, {
      params,
    });
  }

  // /**
  //  * GET /ProductCategories/{id}
  //  * Get a single category by ID.
  //  */
  //
  // /**
  //  * POST /ProductCategories
  //  * Create a new category.
  //  * @returns ID of the newly created category
  //  */
  //
  // /**
  //  * PUT /ProductCategories/{id}
  //  * Update an existing category.
  //  */
  //
  //
  // /**
  //  * DELETE /ProductCategories/{id}
  //  * Delete a category.
  //  */
  // delete(id: number): Observable<void> {
  //   return this.http.delete<void>(`${this.baseUrl}/${id}`);
  // }

  /** GET /Category/{id} */
  getById(id: number): Observable<GetCategoryByIdQueryDto> {
    return this.http.get<GetCategoryByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  // create(payload: UpsertCategoryCommand): Observable<number> {
  //   return this.http.post<number>(this.baseUrl, payload);
  // }

  /** POST /Category */
  create(payload: UpsertCategoryCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, payload);
  }

  update(id: number, payload: UpsertCategoryCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  /** DELETE /Category/{id} */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }


  /**
   * PUT /ProductCategories/{id}/disable
   * Disable a category.
   */
  /*disable(id: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}/disable`, {});
  }

  /**
   * PUT /ProductCategories/{id}/enable
   * Enable a category.
   */
  /*enable(id: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}/enable`, {});
  }*/
}


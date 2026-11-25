import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';
import {
  BranchMapItemDto,
  CreateBranchCommand,
  GetBranchByIdQueryDto,
  ListBranchesQuery,
  ListBranchesResponse,
  UpdateBranchCommand,
} from './branches-api.models';

@Injectable({ providedIn: 'root' })
export class BranchesApiService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Branches`;

  /** GET /Branches */
  list(query: ListBranchesQuery): Observable<ListBranchesResponse> {
    const params = buildHttpParams(query);
    return this.http.get<ListBranchesResponse>(this.baseUrl, { params });
  }

  /** GET /Branches/{id} */
  getById(id: number): Observable<GetBranchByIdQueryDto> {
    return this.http.get<GetBranchByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /** GET /Branches/map — active branches with coordinates for map rendering */
  getMapBranches(): Observable<BranchMapItemDto[]> {
    return this.http.get<BranchMapItemDto[]>(`${this.baseUrl}/map`);
  }

  /** POST /Branches */
  create(command: CreateBranchCommand): Observable<number> {
    return this.http.post<number>(this.baseUrl, command);
  }

  /** PUT /Branches/{id} */
  update(id: number, command: UpdateBranchCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }
}

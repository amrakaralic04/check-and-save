import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';
import { ListPricesQuery, ListPricesResponse } from './prices-api.models';

@Injectable({ providedIn: 'root' })
export class PricesApiService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/Price`;

  list(query: ListPricesQuery): Observable<ListPricesResponse> {
    const params = buildHttpParams(query);
    return this.http.get<ListPricesResponse>(this.baseUrl, { params });
  }
}

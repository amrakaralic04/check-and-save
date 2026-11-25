import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import {
  ListBranchesQuery,
  ListBranchesQueryDto
} from '../../../api-services/branches/branches-api.models';
import { BranchesApiService } from '../../../api-services/branches/branches-api.service';
import { StoresApiService } from '../../../api-services/stores/stores-api.service';
import { CitiesApiService } from '../../../api-services/cities/cities-api.service';
import { ListStoresQueryDto } from '../../../api-services/stores/stores-api.models';
import { ListCitiesQueryDto } from '../../../api-services/cities/cities-api.models';
import { ToasterService } from '../../../core/services/toaster.service';
import { allItemsPaging } from '../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-branches',
  standalone: false,
  templateUrl: './branches.component.html',
  styleUrl: './branches.component.scss'
})
export class BranchesComponent
  extends BaseListPagedComponent<ListBranchesQueryDto, ListBranchesQuery>
  implements OnInit {

  private branchesApi = inject(BranchesApiService);
  private storesApi = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private toaster = inject(ToasterService);
  private router = inject(Router);

  displayedColumns: string[] = [
    'address',
    'store',
    'city',
    'contact',
    'email',
    'isActive',
    'actions'
  ];

  stores: ListStoresQueryDto[] = [];
  cities: ListCitiesQueryDto[] = [];

  private storeById = new Map<number, string>();
  private cityById = new Map<number, string>();

  constructor() {
    super();
    this.request = new ListBranchesQuery();
    this.request.paging.pageSize = 10;
  }

  ngOnInit(): void {
    this.handleNavigationState();
    this.loadFilters();
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.branchesApi.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Greška pri učitavanju poslovnica');
        console.error('Load branches error:', err);
      }
    });
  }

  onSearch(): void {
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  clearSearch(): void {
    this.request.search = '';
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  onFilterChange(): void {
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  resetFilters(): void {
    this.request.search = '';
    this.request.storeEntityId = null;
    this.request.cityEntityId = null;
    this.request.onlyActive = null;
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  onCreate(): void {
    this.router.navigate(['/admin/branches/add']);
  }

  onEdit(branch: ListBranchesQueryDto): void {
    this.router.navigate(['/admin/branches', branch.id, 'edit']);
  }

  getStoreName(storeId: number): string {
    return this.storeById.get(storeId) ?? `#${storeId}`;
  }

  getCityName(cityId: number): string {
    return this.cityById.get(cityId) ?? `#${cityId}`;
  }

  private handleNavigationState(): void {
    const navigationState =
      this.router.getCurrentNavigation()?.extras.state ?? history.state;
    const successMessage = navigationState?.successMessage as string | undefined;

    if (successMessage) {
      this.toaster.success(successMessage);
    }
  }

  private loadFilters(): void {
    this.storesApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => {
        this.stores = response.items;
        this.storeById = new Map(
          response.items.map((store) => [store.id, store.name])
        );
      },
      error: (err) => {
        this.toaster.error('Greška pri učitavanju prodavnica');
        console.error('Load stores error:', err);
      }
    });

    this.citiesApi.list({ paging: allItemsPaging }).subscribe({
      next: (response) => {
        this.cities = response.items;
        this.cityById = new Map(
          response.items.map((city) => [city.id, city.name])
        );
      },
      error: (err) => {
        this.toaster.error('Greška pri učitavanju gradova');
        console.error('Load cities error:', err);
      }
    });
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { DialogButton } from '../../shared/models/dialog-config.model';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { ToasterService } from '../../../core/services/toaster.service';
import {
  ListStoresQuery,
  ListStoresQueryDto
} from '../../../api-services/stores/stores-api.models';
import { StoresApiService } from '../../../api-services/stores/stores-api.service';
import { CitiesApiService } from '../../../api-services/cities/cities-api.service';
import { ListCitiesQueryDto } from '../../../api-services/cities/cities-api.models';
import { largePaging } from '../../../core/models/paging/paging-utils';

@Component({
  selector: 'app-stores',
  standalone: false,
  templateUrl: './stores.component.html',
  styleUrl: './stores.component.scss'
})
export class StoresComponent
  extends BaseListPagedComponent<ListStoresQueryDto, ListStoresQuery>
  implements OnInit {

  private api = inject(StoresApiService);
  private citiesApi = inject(CitiesApiService);
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);
  private toaster = inject(ToasterService);

  displayedColumns: string[] = [
    'name',
    'contact',
    'email',
    'city',
    'isActive',
    'actions'
  ];

  cities: ListCitiesQueryDto[] = [];
  private cityById = new Map<number, string>();

  constructor() {
    super();
    this.request = new ListStoresQuery();
  }

  ngOnInit(): void {
    this.loadCities();
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Failed to load stores');
        console.error('Load stores error:', err);
      }
    });
  }

  onCreate(): void {
    this.router.navigate(['/admin/stores/add']);
  }

  onEdit(store: ListStoresQueryDto): void {
    this.router.navigate(['/admin/stores', store.id, 'edit']);
  }

  onDelete(store: ListStoresQueryDto): void {
    this.dialogHelper.confirmDelete(store.name).subscribe(result => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(store);
      }
    });
  }

  getCityName(cityId: number): string {
    return this.cityById.get(cityId) ?? `#${cityId}`;
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

  private loadCities(): void {
    this.citiesApi.list({ paging: largePaging }).subscribe({
      next: (response) => {
        this.cities = response.items;
        this.cityById = new Map(this.cities.map(city => [city.id, city.name]));
      },
      error: (err) => {
        this.toaster.error('Failed to load cities');
        console.error('Load cities error:', err);
      }
    });
  }

  private performDelete(store: ListStoresQueryDto): void {
    this.startLoading();

    this.api.delete(store.id).subscribe({
      next: () => {
        this.toaster.success('Store deleted successfully');
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();
        this.dialogHelper.showError(
          'DIALOGS.TITLES.ERROR',
          'STORES.DIALOGS.ERROR_DELETE'
        ).subscribe();
        console.error('Delete store error:', err);
      }
    });
  }
}

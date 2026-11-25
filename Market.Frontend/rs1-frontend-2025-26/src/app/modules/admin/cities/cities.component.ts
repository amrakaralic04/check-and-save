import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  ListCitiesQuery,
  ListCitiesQueryDto
} from '../../../api-services/cities/cities-api.models';
import { CitiesApiService } from '../../../api-services/cities/cities-api.service';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { DialogButton } from '../../shared/models/dialog-config.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-cities',
  standalone: false,
  templateUrl: './cities.component.html',
  styleUrl: './cities.component.scss'
})
export class CitiesComponent
  extends BaseListPagedComponent<ListCitiesQueryDto, ListCitiesQuery>
  implements OnInit {

  private api = inject(CitiesApiService);
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);
  private toaster = inject(ToasterService);

  displayedColumns: string[] = ['name', 'postalCode', 'actions'];

  constructor() {
    super();
    this.request = new ListCitiesQuery();
  }

  ngOnInit(): void {
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
        this.stopLoading('Greška pri učitavanju gradova');
        console.error('Load cities error:', err);
      }
    });
  }

  onCreate(): void {
    this.router.navigate(['/admin/cities/add']);
  }

  onEdit(city: ListCitiesQueryDto): void {
    this.router.navigate(['/admin/cities', city.id, 'edit']);
  }

  onDelete(city: ListCitiesQueryDto): void {
    this.dialogHelper.confirmDelete(city.name).subscribe(result => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(city);
      }
    });
  }

  private performDelete(city: ListCitiesQueryDto): void {
    this.startLoading();

    this.api.delete(city.id).subscribe({
      next: () => {
        this.toaster.success('Grad je uspješno obrisan');
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();
        this.dialogHelper.showError(
          'Greška',
          'Došlo je do greške pri brisanju grada'
        ).subscribe();
        console.error('Delete city error:', err);
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
}

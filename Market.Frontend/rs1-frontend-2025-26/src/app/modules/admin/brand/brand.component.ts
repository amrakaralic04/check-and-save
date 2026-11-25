import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListBrandsQuery, ListBrandsQueryDto } from '../../../api-services/brand/brand-api.model';
import { BrandsApiService } from '../../../api-services/brand/brand-api.service';


import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { DialogButton } from '../../shared/models/dialog-config.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-brands',
  standalone: false,
  templateUrl: './brand.component.html',
  styleUrl: './brand.component.scss'

})
export class BrandComponent
  extends BaseListPagedComponent<ListBrandsQueryDto, ListBrandsQuery>
  implements OnInit {

  private api = inject(BrandsApiService);
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);
  private toaster = inject(ToasterService);

  displayedColumns: string[] = ['name', 'actions'];

  constructor() {
    super();

    this.request = {
      search: '',
      paging: {
        page: 1,
        pageSize: 10
      }
    };
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
        this.stopLoading('Greška pri učitavanju brendova');
        console.error('Load brands error:', err);
      }
    });
  }

  onCreate(): void {
    this.router.navigate(['/admin/brands/add']);
  }

  onEdit(brand: ListBrandsQueryDto): void {
    this.router.navigate(['/admin/brands', brand.id, 'edit']);
  }

  onDelete(brand: ListBrandsQueryDto): void {
    this.dialogHelper.confirmDelete(brand.name).subscribe(result => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(brand);
      }
    });
  }

  private performDelete(brand: ListBrandsQueryDto): void {
    this.startLoading();

    this.api.delete(brand.id).subscribe({
      next: () => {
        this.toaster.success('Brend je uspješno obrisan');
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();
        this.dialogHelper.showError(
          'Greška',
          'Došlo je do greške pri brisanju brenda'
        ).subscribe();
        console.error('Delete brand error:', err);
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

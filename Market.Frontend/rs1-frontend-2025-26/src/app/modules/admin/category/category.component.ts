import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import {
  ListCategoriesQuery,
  ListCategoriesQueryDto
} from '../../../api-services/category/category-api.model';
import { CategoriesApiService } from '../../../api-services/category/category-api.service';

import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { DialogButton } from '../../shared/models/dialog-config.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-categories',
  standalone: false,
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
})
export class CategoryComponent
  extends BaseListPagedComponent<ListCategoriesQueryDto, ListCategoriesQuery>
  implements OnInit
{
  private api = inject(CategoriesApiService);
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);
  private toaster = inject(ToasterService);

  displayedColumns: string[] = ['name', 'description', 'actions'];

  constructor() {
    super();

    this.request = {
      search: '',
      paging: {
        page: 1,
        pageSize: 10,
      },
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
        this.stopLoading('Greška pri učitavanju kategorija');
        console.error('Load categories error:', err);
      },
    });
  }

  onCreate(): void {
    this.router.navigate(['/admin/categories/add']);
  }

  onEdit(category: ListCategoriesQueryDto): void {
    this.router.navigate(['/admin/categories', category.id, 'edit']);
  }

  onDelete(category: ListCategoriesQueryDto): void {
    this.dialogHelper.confirmDelete(category.name).subscribe((result) => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(category);
      }
    });
  }

  private performDelete(category: ListCategoriesQueryDto): void {
    this.startLoading();

    this.api.delete(category.id).subscribe({
      next: () => {
        this.toaster.success('Kategorija je uspješno obrisana');
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();
        this.dialogHelper
          .showError('Greška', 'Došlo je do greške pri brisanju kategorije')
          .subscribe();
        console.error('Delete category error:', err);
      },
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

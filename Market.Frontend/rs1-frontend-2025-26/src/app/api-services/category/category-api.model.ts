import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export interface ListCategoriesQueryDto {
  id: number;
  name: string;
  description?: string | null;
}

export interface GetCategoryByIdQueryDto {
  id: number;
  name: string;
  description?: string | null;
}

export interface ListCategoriesQuery extends BasePagedQuery {
  search?: string;
}

export type ListCategoriesQueryResponse = PageResult<ListCategoriesQueryDto>;

export interface UpsertCategoryCommand {
  name: string;
  description?: string | null;
}

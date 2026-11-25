import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export interface ListBrandsQueryDto {
  id: number;
  name: string;
  description: string | null;
}
export interface GetBrandByIdQueryDto {
  id: number;
  name: string;
  description?: string | null;
}

export interface ListBrandsQuery extends BasePagedQuery {
  search?: string;
}

export type ListBrandsQueryResponse = PageResult<ListBrandsQueryDto>;

export interface UpsertBrandCommand {
  name: string;
  description?: string | null;
}

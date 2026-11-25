import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

export class ListPricesQuery extends BasePagedQuery {
  productEntityId?: number | null;
}

export interface ListPricesQueryDto {
  id: number;
  productEntityId: number;
  amount: number;
  dateUpdated: string;
}

export type ListPricesResponse = PageResult<ListPricesQueryDto>;

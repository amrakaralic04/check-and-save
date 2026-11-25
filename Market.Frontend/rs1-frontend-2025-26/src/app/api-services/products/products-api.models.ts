import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

export class ListProductsQuery extends BasePagedQuery {
  search?: string | null;
  branchEntityId?: number | null;
  categoryEntityId?: number | null;
  brandEntityId?: number | null;
  storeEntityId?: number | null;
}

export interface ListProductsQueryDto {
  id: number;
  branchEntityId: number;
  categoryEntityId: number;
  brandEntityId: number;
  name: string;
  storeEntityId: number;
  storeLabel?: string | null;
  lowestPrice?: number | null;
  imageUrl?: string | null;
  dateAdded: string;
}

export interface GetProductByIdQueryDto {
  id: number;
  storeEntityId: number;
  branchEntityId: number;
  categoryEntityId: number;
  brandEntityId: number;
  name: string;
  description: string;
  imageURL: string;
  dateAdded: string;
}

export type ListProductsResponse = PageResult<ListProductsQueryDto>;

export interface CompareProductsQueryDto {
  products: CompareProductDto[];
}

export interface CompareProductDto {
  publicId: string;
  id: number;
  name: string;
  description: string;
  imageURL: string;
  dateAdded: string;
  categoryEntityId: number;
  categoryName: string;
  brandEntityId: number;
  brandName: string;
  prices: CompareStorePriceDto[];
}

export interface CompareStorePriceDto {
  productId: number;
  productPublicId: string;
  storeEntityId: number;
  storeName: string;
  branchEntityId: number;
  branchAddress: string;
  amount?: number | null;
  dateUpdated?: string | null;
}

// === COMMANDS (WRITE) ===

export interface CreateProductCommand {
  storeEntityId: number;
  branchEntityId: number;
  categoryEntityId: number;
  brandEntityId: number;
  name: string;
  description: string;
  imageURL: string;
  dateAdded: string | Date;
}

export interface UpdateProductCommand {
  storeEntityId: number;
  branchEntityId: number;
  categoryEntityId: number;
  brandEntityId: number;
  name: string;
  description: string;
  imageURL: string;
}

// === IMAGE UPLOAD ===

export interface UploadProductImageDto {
  imageUrl: string;
}

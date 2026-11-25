import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /Store
 * Corresponds to: ListStoresQuery.cs
 */
export class ListStoresQuery extends BasePagedQuery {
  search?: string | null;
  cityEntityId?: number | null;
  onlyActive?: boolean | null;
}

/**
 * Response item for GET /Store
 * Corresponds to: ListStoresQueryDto.cs
 */
export interface ListStoresQueryDto {
  id: number;
  name: string;
  contact: string;
  email: string;
  isActive: boolean;
  cityEntityId: number;
}

/**
 * Response for GET /Store/{id}
 * Corresponds to: GetStoreByIdQueryDto.cs
 */
export interface GetStoreByIdQueryDto {
  id: number;
  name: string;
  contact: string;
  email: string;
  isActive: boolean;
  cityEntityId: number;
}

/**
 * Paged response for GET /Store
 */
export type ListStoresResponse = PageResult<ListStoresQueryDto>;

// === COMMANDS (WRITE) ===

/**
 * Command for POST /Store
 * Corresponds to: CreateStoreCommand.cs
 */
export interface CreateStoreCommand {
  name: string;
  contact: string;
  email: string;
  cityEntityId: number;
}

/**
 * Command for PUT /Store/{id}
 * Corresponds to: UpdateStoreCommand.cs
 */
export interface UpdateStoreCommand {
  name: string;
  contact: string;
  email: string;
  cityEntityId: number;
  isActive: boolean;
}

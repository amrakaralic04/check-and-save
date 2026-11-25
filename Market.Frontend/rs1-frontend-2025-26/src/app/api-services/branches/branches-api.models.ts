import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

export class ListBranchesQuery extends BasePagedQuery {
  storeEntityId?: number | null;
  cityEntityId?: number | null;
  onlyActive?: boolean | null;
  search?: string | null;
}

export interface ListBranchesQueryDto {
  id: number;
  storeEntityId: number;
  cityEntityId: number;
  address: string;
  contact: string;
  email: string;
  isActive: boolean;
}

export type ListBranchesResponse = PageResult<ListBranchesQueryDto>;

/**
 * Response item for GET /Branches/map
 * Corresponds to: BranchMapItemDto.cs
 */
export interface BranchMapItemDto {
  id: number;
  storeName: string;
  address: string;
  contact: string;
  email: string;
  latitude: number;
  longitude: number;
}

// === COMMANDS (WRITE) ===

export interface CreateBranchCommand {
  storeEntityId: number;
  cityEntityId: number;
  address: string;
  contact: string;
  email: string;
}

export interface UpdateBranchCommand {
  storeEntityId: number;
  cityEntityId: number;
  address: string;
  contact: string;
  email: string;
  isActive: boolean;
}

export interface GetBranchByIdQueryDto {
  id: number;
  storeEntityId: number;
  cityEntityId: number;
  address: string;
  contact: string;
  email: string;
  isActive: boolean;
}

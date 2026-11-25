import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /City
 * Corresponds to: ListCitiesQuery.cs
 */
export class ListCitiesQuery extends BasePagedQuery {
  search?: string | null;
}

/**
 * Response item for GET /City
 * Corresponds to: ListCitiesQueryDto.cs
 */
export interface ListCitiesQueryDto {
  id: number;
  name: string;
  postalCode: number;
}

/**
 * Response for GET /City/{id}
 * Corresponds to: GetCityByIdQueryDto.cs
 */
export interface GetCityByIdQueryDto {
  id: number;
  name: string;
  postalCode: number;
}

/**
 * Paged response for GET /City
 */
export type ListCitiesResponse = PageResult<ListCitiesQueryDto>;

// === COMMANDS (WRITE) ===

/**
 * Command for POST /City
 * Corresponds to: CreateCityCommand.cs
 */
export interface CreateCityCommand {
  name: string;
  postalCode: number;
}

/**
 * Command for PUT /City/{id}
 * Corresponds to: UpdateCityCommand.cs
 */
export interface UpdateCityCommand {
  name: string;
  postalCode: number;
}

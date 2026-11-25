import { PageResult } from '../../core/models/paging/page-result';

export interface FavouriteProductCardDto {
  id: number;
  publicId?: string;
  productEntityId: number;
  name: string;
  price: number | null;
  imageUrl: string | null;
}
export interface FavoriteListItemDto {
  id: number;
  publicId?: string;
  publicUserEntityId: number;
  productEntityId: number;
  dateAdded: string;
  name?: string | null;
  price?: number | null;
  imageUrl?: string | null;
}

export type FavoritesListResponse = PageResult<FavoriteListItemDto>;

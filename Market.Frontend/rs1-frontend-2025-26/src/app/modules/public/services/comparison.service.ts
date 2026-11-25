import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ComparisonService {
  private readonly selectedProductIdsSubject = new BehaviorSubject<number[]>([]);
  readonly selectedProductIds$ = this.selectedProductIdsSubject.asObservable();

  get selectedProductIds(): number[] {
    return this.selectedProductIdsSubject.value;
  }

  setSelectedProductIds(productIds: number[]): void {
    this.selectedProductIdsSubject.next(this.normalizeProductIds(productIds));
  }

  toggleProduct(productId: number): void {
    const normalizedId = Number(productId);

    if (!Number.isFinite(normalizedId) || normalizedId <= 0) {
      return;
    }

    const selectedIds = this.selectedProductIds;
    const nextIds = selectedIds.includes(normalizedId)
      ? selectedIds.filter((id) => id !== normalizedId)
      : [...selectedIds, normalizedId];
    this.selectedProductIdsSubject.next(nextIds);
  }

  isSelected(productId: number): boolean {
    return this.selectedProductIds.includes(productId);
  }

  clear(): void {
    this.selectedProductIdsSubject.next([]);
  }
  private normalizeProductIds(productIds: number[]): number[] {
    return productIds
      .map((id) => Number(id))
      .filter((id) => Number.isFinite(id) && id > 0)
      .filter((id, index, ids) => ids.indexOf(id) === index);
  }
}

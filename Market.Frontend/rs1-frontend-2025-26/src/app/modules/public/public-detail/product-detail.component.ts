import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { findPublicProductById, PublicProduct } from '../models/public-product.model';

@Component({
  selector: 'app-product-detail',
  standalone: false,
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss',
})
export class ProductDetailComponent {
  protected readonly product: PublicProduct | null;

  constructor(private readonly route: ActivatedRoute) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.product = Number.isFinite(id) ? findPublicProductById(id) : null;
  }
}

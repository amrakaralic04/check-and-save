import { NgModule } from '@angular/core';

import { PublicRoutingModule } from './public-routing-module';
import { PublicLayoutComponent } from './public-layout/public-layout.component';
import { SearchProductsComponent } from './search-products/search-products.component';
import { ProductDetailComponent } from './public-detail/product-detail.component';
import { ProductComparisonComponent } from './product-comparison/product-comparison.component';
import { ProductImageCarouselComponent } from './product-image-carousel/product-image-carousel.component';
import { SharedModule } from '../shared/shared-module';

@NgModule({
  declarations: [
    PublicLayoutComponent,
    SearchProductsComponent,
    ProductDetailComponent,
    ProductComparisonComponent,
  ],
  imports: [
    SharedModule,
    PublicRoutingModule,
    ProductImageCarouselComponent,
  ]
})
export class PublicModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PublicLayoutComponent } from './public-layout/public-layout.component';
import { SearchProductsComponent } from './search-products/search-products.component';
import { ProductDetailComponent } from './public-detail/product-detail.component';
import { ProductComparisonComponent } from './product-comparison/product-comparison.component';
const routes: Routes = [
  {
    path: '',
    component: PublicLayoutComponent,
    children: [
      {
        path: '',
        component: SearchProductsComponent
      },
      { path: 'product/:id', component: ProductDetailComponent },
      { path: 'compare', component: ProductComparisonComponent },
      { path: 'compare/:leftId/:rightId', component: ProductComparisonComponent },
      { path: '**', redirectTo: '' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}

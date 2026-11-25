import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import {AdminSettingsComponent} from './admin-settings/admin-settings.component';
import {CategoryComponent} from './category/category.component';
import {CitiesComponent} from './cities/cities.component';
import {CitiesAddComponent} from './cities/cities-add/cities-add.component';
import {CitiesEditComponent} from './cities/cities-edit/cities-edit.component';
import {StoresComponent} from './stores/stores.component';
import {StoresAddComponent} from './stores/stores-add/stores-add.component';
import {StoresEditComponent} from './stores/stores-edit/stores-edit.component';
import { BrandComponent } from './brand/brand.component';
import {BrandAddComponent} from './brand/brand-add/brand-add.component';
import {BrandEditComponent} from './brand/brand-edit/brand-edit.component';
import {CategoryAddComponent} from './category/category-add/category-add.component';
import {CategoryEditComponent} from './category/category-edit/category-edit.component';
import { ProductsComponent } from './products/products.component';
import { ProductsAddComponent } from './products/products-add/products-add.component';
import { ProductsEditComponent } from './products/products-edit/products-edit.component';
import { BranchesComponent } from './branches/branches.component';
import { BranchesAddComponent } from './branches/branches-add/branches-add.component';
import { BranchesEditComponent } from './branches/branches-edit/branches-edit.component';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [

      {
        path: 'settings',
        component: AdminSettingsComponent,
      },
      {
        path: 'categories',
        component: CategoryComponent,
      },
      {
        path: 'categories/add',
        component:CategoryAddComponent,
      },
      {
        path: 'categories/:id/edit',
        component: CategoryEditComponent
      },
      {
        path: 'brands',
        component: BrandComponent
      },
      {
        path: 'brands/add',
        component: BrandAddComponent,
      },
      {
        path: 'brands/:id/edit',
        component: BrandEditComponent,
      },
      {
        path: 'cities',
        component: CitiesComponent,
      },
      {
        path: 'cities/add',
        component: CitiesAddComponent,
      },
      {
        path: 'cities/:id/edit',
        component: CitiesEditComponent,
      },
      {
        path: 'stores',
        component: StoresComponent,
      },
      {
        path: 'stores/add',
        component: StoresAddComponent,
      },
      {
        path: 'stores/:id/edit',
        component: StoresEditComponent
      },
      {
        path: 'products',
        component: ProductsComponent
      },
      {
        path: 'products/add',
        component: ProductsAddComponent
      },
      {
        path: 'products/:id/edit',
        component: ProductsEditComponent
      },
      {
        path: 'branches/add',
        component: BranchesAddComponent
      },
      {
        path: 'branches/:id/edit',
        component: BranchesEditComponent
      },
      {
        path: 'branches',
        component: BranchesComponent
      },
      // default admin route → /admin/products
      {
        path: '',
        redirectTo: 'products',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}

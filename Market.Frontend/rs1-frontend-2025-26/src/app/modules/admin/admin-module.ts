import { NgModule } from '@angular/core';

import { AdminRoutingModule } from './admin-routing-module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AdminSettingsComponent } from './admin-settings/admin-settings.component';
import { SharedModule } from '../shared/shared-module';
import { CategoryComponent } from './category/category.component';
import { CitiesComponent } from './cities/cities.component';
import { CitiesAddComponent } from './cities/cities-add/cities-add.component';
import { CitiesEditComponent } from './cities/cities-edit/cities-edit.component';
import { StoresComponent } from './stores/stores.component';
import { StoresAddComponent } from './stores/stores-add/stores-add.component';
import { StoresEditComponent } from './stores/stores-edit/stores-edit.component';
import { BrandComponent } from './brand/brand.component';
import { ProductsComponent } from './products/products.component';
import { ProductsAddComponent } from './products/products-add/products-add.component';
import { ProductsEditComponent } from './products/products-edit/products-edit.component';
import { BranchesComponent } from './branches/branches.component';
import { BranchesAddComponent } from './branches/branches-add/branches-add.component';
import { BranchesEditComponent } from './branches/branches-edit/branches-edit.component';

import { MatIconModule } from '@angular/material/icon';
import {BrandAddComponent} from './brand/brand-add/brand-add.component';
import {BrandEditComponent} from './brand/brand-edit/brand-edit.component';
import {CategoryAddComponent} from './category/category-add/category-add.component';
import {CategoryEditComponent} from './category/category-edit/category-edit.component';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    AdminSettingsComponent,
    CategoryComponent,
    CategoryAddComponent,
    CategoryEditComponent,
    CitiesComponent,
    CitiesAddComponent,
    CitiesEditComponent,
    StoresComponent,
    StoresAddComponent,
    StoresEditComponent,
    BrandComponent,
    BrandAddComponent,
    BrandEditComponent,
    ProductsComponent,
    ProductsAddComponent,
    ProductsEditComponent,
    BranchesComponent,
    BranchesAddComponent,
    BranchesEditComponent
  ],
  imports: [
    AdminRoutingModule,
    SharedModule,
    MatIconModule,
    //
  ]
})
export class AdminModule { }

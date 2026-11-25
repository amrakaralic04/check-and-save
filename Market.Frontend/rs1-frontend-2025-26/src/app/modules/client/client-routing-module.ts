import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './client-pages/home-page/home.component';
import { FavoritesComponent } from './client-pages/favorites-page/favorites.component';
import { MapComponent } from './client-pages/map-page/map.component';
import { ProfileComponent } from './client-pages/profile-page/profile.component';
import { EditProfileComponent } from './client-pages/profile-page/edit-profile.component';

import { myAuthData, myAuthGuard } from '../../core/guards/my-auth-guard';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  {
    path: 'favorites',
    component: FavoritesComponent,
    canActivate: [myAuthGuard],
    data: myAuthData({ requireAuth: true, requirePublicUser: true }),
  },
  { path: 'map', component: MapComponent },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [myAuthGuard],
    data: myAuthData({ requireAuth: true, requirePublicUser: true }),
  },
  {
    path: 'profile/edit',
    component: EditProfileComponent,
    canActivate: [myAuthGuard],
    data: myAuthData({ requireAuth: true, requirePublicUser: true }),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}

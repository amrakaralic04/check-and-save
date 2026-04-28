import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { take } from 'rxjs';

import { FavoritesService } from '../../../shared/services/favorites.services';
import { DialogHelperService } from '../../../shared/services/dialog-helper.service';
import { DialogButton, DialogType } from '../../../shared/models/dialog-config.model';
import { FavouritesApiService } from '../../../../api-services/favourites/favourites-api.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { FavouriteProductCardDto } from '../../../../api-services/favourites/favourites-api.models';

@Component({
  selector: 'app-client-favorites',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss'],
})
export class FavoritesComponent {
  private favoritesService = inject(FavoritesService);
  private dialogHelper = inject(DialogHelperService);
  private favouritesApi = inject(FavouritesApiService);
  private toaster = inject(ToasterService);

  favorites = this.favoritesService.favorites;

  confirmRemove(item: FavouriteProductCardDto): void {
    this.dialogHelper
      .open({
        type: DialogType.WARNING,
        title: 'Ukloniti iz omiljenih?',
        message: `Proizvod "${item.name}" će biti uklonjen iz vaše liste omiljenih proizvoda.`,
        icon: 'delete_forever',
        buttons: [
          { type: DialogButton.CANCEL },
          { type: DialogButton.DELETE, color: 'warn' }
        ]
      })
      .pipe(take(1))
      .subscribe(result => {
        if (result?.button !== DialogButton.DELETE) {
          return;
        }

        this.favouritesApi.delete(item.publicId).pipe(take(1)).subscribe({
          next: () => {
            this.favoritesService.removeByPublicId(item.publicId);
            this.toaster.success('Proizvod je uspješno uklonjen iz omiljenih.');
          },
          error: () => {
            this.toaster.error('Brisanje iz omiljenih nije uspjelo. Pokušajte ponovo.');
          }
        });
      });
  }

  clearAll(): void {
    this.favoritesService.clear();
  }

  trackByPublicId(_: number, item: FavouriteProductCardDto): string {
    return item.publicId;
  }
}

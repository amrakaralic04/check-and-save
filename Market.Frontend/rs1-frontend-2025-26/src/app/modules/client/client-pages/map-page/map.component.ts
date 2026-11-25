import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  inject,
  signal,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import * as L from 'leaflet';

import { BranchesApiService } from '../../../../api-services/branches/branches-api.service';
import { BranchMapItemDto } from '../../../../api-services/branches/branches-api.models';

// Fix za Leaflet default marker ikone (Webpack asset problem)
const defaultIcon = L.icon({
  iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
  iconRetinaUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon-2x.png',
  shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
});
L.Marker.prototype.options.icon = defaultIcon;

@Component({
  selector: 'app-client-map',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('mapContainer', { static: true }) mapContainer!: ElementRef;

  private branchesApi = inject(BranchesApiService);

  private map: L.Map | null = null;
  private markers: L.Marker[] = [];

  branches = signal<BranchMapItemDto[]>([]);
  selectedBranch = signal<BranchMapItemDto | null>(null);
  isLoading = signal(true);
  hasError = signal(false);

  ngOnInit(): void {
    this.loadBranches();
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.initMap();
    }, 0);
  }

  ngOnDestroy(): void {
    this.map?.remove();
    this.map = null;
  }

  private initMap(): void {
    this.map = L.map(this.mapContainer.nativeElement, {
      center: [43.3438, 17.8078], // Mostar
      zoom: 13,
      zoomControl: true,
    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
      maxZoom: 19,
    }).addTo(this.map);

    // Fix za nepotpuno renderovanje tiles-a
    setTimeout(() => {
      this.map?.invalidateSize();
    }, 100);
  }

  private loadBranches(): void {
    this.branchesApi.getMapBranches().subscribe({
      next: (data) => {
        this.branches.set(data);
        this.isLoading.set(false);
        this.renderMarkers(data);
      },
      error: (err) => {
        console.error('Failed to load map branches:', err);
        this.hasError.set(true);
        this.isLoading.set(false);
      },
    });
  }

  private renderMarkers(branches: BranchMapItemDto[]): void {
    if (!this.map) return;

    // Ukloni stare markere
    this.markers.forEach((m) => m.remove());
    this.markers = [];

    branches.forEach((branch) => {
      const marker = L.marker([branch.latitude, branch.longitude])
        .addTo(this.map!)
        .bindPopup(this.buildPopupContent(branch));

      marker.on('click', () => {
        this.selectedBranch.set(branch);
      });

      this.markers.push(marker);
    });

    // Fit mapa na sve markere ako ih ima
    if (branches.length > 0) {
      const group = L.featureGroup(this.markers);
      this.map.fitBounds(group.getBounds().pad(0.1));
    }
  }

  private buildPopupContent(branch: BranchMapItemDto): string {
    return `
      <div class="leaflet-popup-custom">
        <strong>${branch.storeName}</strong>
        <p>${branch.address}</p>
        <p>${branch.contact}</p>
        <p>${branch.email}</p>
      </div>
    `;
  }

  onCardClick(branch: BranchMapItemDto): void {
    this.selectedBranch.set(branch);
    if (this.map) {
      this.map.setView([branch.latitude, branch.longitude], 16);
      const marker = this.markers[this.branches().indexOf(branch)];
      marker?.openPopup();
    }
  }

  retryLoad(): void {
    this.hasError.set(false);
    this.isLoading.set(true);
    this.loadBranches();
  }
}

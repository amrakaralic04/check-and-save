import {
  Component,
  Input,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-image-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-image-carousel.component.html',
  styleUrls: ['./product-image-carousel.component.scss'],
})
export class ProductImageCarouselComponent implements OnInit, OnDestroy {
  @Input() images: string[] = [];
  @Input() autoPlay = true;
  @Input() intervalMs = 3000;

  currentIndex = 0;
  private timer: ReturnType<typeof setInterval> | null = null;

  ngOnInit(): void {
    if (this.autoPlay && this.images.length > 1) {
      this.startAutoPlay();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoPlay();
  }

  prev(): void {
    this.currentIndex =
      (this.currentIndex - 1 + this.images.length) % this.images.length;
    this.restartAutoPlay();
  }

  next(): void {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
    this.restartAutoPlay();
  }

  goTo(index: number): void {
    this.currentIndex = index;
    this.restartAutoPlay();
  }

  private startAutoPlay(): void {
    this.timer = setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }, this.intervalMs);
  }

  private stopAutoPlay(): void {
    if (this.timer !== null) {
      clearInterval(this.timer);
      this.timer = null;
    }
  }

  private restartAutoPlay(): void {
    if (this.autoPlay && this.images.length > 1) {
      this.stopAutoPlay();
      this.startAutoPlay();
    }
  }
}

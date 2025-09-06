import { Component, computed, inject, OnInit, signal, viewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BreakpointObserver, Breakpoints, LayoutModule } from '@angular/cdk/layout';
import { MatDrawerMode, MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

import { ComponentBase } from './shared/component.base';

@Component({
  selector: 'app-root',
  imports: [
    RouterModule,
    LayoutModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatDividerModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent extends ComponentBase implements OnInit {
  private readonly breakpointObserver = inject(BreakpointObserver);

  private readonly sidenav = viewChild<MatSidenav>('sidenav');

  isSmallSize = signal(false);
  sidenavMode = computed<MatDrawerMode>(() => (this.isSmallSize() ? 'over' : 'side'));

  ngOnInit(): void {
    this.breakpointObserver
      .observe(Breakpoints.Handset)
      .pipe(this.takeUntilDestroyed())
      .subscribe((result) => {
        if (!this.isSmallSize() && result.matches) {
          // Close the sidenav when switching to small size
          this.sidenav()?.close();
        } else if (this.isSmallSize() && !result.matches) {
          // Open the sidenav when switching to large size
          this.sidenav()?.open();
        }

        this.isSmallSize.set(result.matches);
      });
  }
}

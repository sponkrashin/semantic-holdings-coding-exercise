import { DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export abstract class ComponentBase {
  protected readonly destroyRef = inject(DestroyRef);

  protected takeUntilDestroyed<T>() {
    return takeUntilDestroyed<T>(this.destroyRef);
  }
}

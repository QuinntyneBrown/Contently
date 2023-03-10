import { OnDestroy } from '@angular/core';
import { inject } from '@angular/core';
import { ElementRef } from '@angular/core';
import { Directive } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, Subject, takeUntil, tap } from 'rxjs';

const listViewCssClass = 'g-list-detail-container--list-view';

@Directive({
  selector: '[gListDetail]',
  standalone: true,
  host: {
    class: 'g-list-detail-container'
  }
})
export class ListDetailDirective implements OnDestroy {

  private readonly _destroyed$ = new Subject();
  private readonly _activatedRoute = inject(ActivatedRoute);
  private readonly _elementRef = inject(ElementRef);

  constructor() {
    this._activatedRoute.url
    .pipe(
      takeUntil(this._destroyed$),
      map(url => url.length == 0),
      tap(listView => {

        if(listView && !this._elementRef.nativeElement.classList.contains(listViewCssClass)) {
          this._elementRef.nativeElement.classList.add(listViewCssClass);
        }

        if(!listView) {
          this._elementRef.nativeElement.classList.remove(listViewCssClass)
        }
      })
      ).subscribe();
  }

  ngOnDestroy(): void {
    this._destroyed$.next(null);
    this._destroyed$.complete();
  }
}

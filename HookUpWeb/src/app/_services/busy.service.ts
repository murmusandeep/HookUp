import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  bustRequestCount = 0;

  constructor(private spinner: NgxSpinnerService) { }

  busy() {
    this.bustRequestCount++;
    this.spinner.show(undefined, {
      type: 'ball-atom',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333'
    })
  }

  idle() {
    this.bustRequestCount--;
    if (this.bustRequestCount <= 0) {
      this.bustRequestCount = 0;
      this.spinner.hide();
    }
  }
}

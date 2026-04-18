import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toastEvents = new Subject<{message: string, type: 'success' | 'error'}>();

  show(message: string, type: 'success' | 'error' = 'success') {
    this.toastEvents.next({ message, type });
  }
}
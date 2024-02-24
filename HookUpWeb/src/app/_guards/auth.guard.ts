import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { MessageService } from 'primeng/api';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {

  const accountService = inject(AccountService);
  const messageService = inject(MessageService);

  return accountService.currentUser$.pipe(
    map(user => {
      if (user) return true;
      else {
        messageService.add({ severity: 'warn', summary: 'Warn', detail: 'You are not Authorize' });
        return false;
      }
    })
  );
};

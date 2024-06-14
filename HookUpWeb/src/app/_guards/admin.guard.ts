import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';
import { MessageService } from 'primeng/api';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const messageService = inject(MessageService);

  return accountService.currentUser$.pipe(
    map((user) => {
      if (!user) return false;
      if (user.roles.includes('Admin') || user.roles.includes('Moderator')) {
        return true;
      }
      else {
        messageService.add({ severity: 'error', summary: 'Error', detail: 'You are not Authorize', life: 1000 });
        return false;
      }
    })
  );
};

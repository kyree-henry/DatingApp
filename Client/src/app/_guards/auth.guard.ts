import { CanActivateFn, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';


export const authGuard: CanActivateFn = () : Observable<boolean> =>  {

    const accountService = inject(AccountService);
    const toastr = inject(ToastrService);
    const router = inject(Router);
    
    return accountService.currentUser$.pipe(
      map(user => {
        if (user) return true;
         else{
          toastr.error('You shall not pass!');
          return false;
        }
      })
    )
    
}
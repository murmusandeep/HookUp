import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private messageService: MessageService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modelStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modelStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modelStateErrors.flat();
              } else {
                this.messageService.add({
                  severity: 'error',
                  summary: error.status.toString(),
                  detail: error.error,
                });
              }
              break;
            case 401:
              this.messageService.add({
                severity: 'error',
                summary: error.status.toString(),
                detail: 'Unauthorised',
              });
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = {
                state: {
                  error: error.error,
                },
              };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.messageService.add({
                severity: 'error',
                summary: error.status.toString(),
                detail: 'Something Went Wrong',
              });
              console.log(error);
              break;
          }
        }
        throw error;
      })
    );
  }
}

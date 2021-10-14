/* eslint-disable @typescript-eslint/naming-convention */
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { toCamel } from '../helpers/to-camel-case.helper';
import { ServerException } from '../models/server-exception';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor() {}

  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        console.log(err.error);
        if (err.error) {
          const exception = toCamel(err.error) as ServerException;
          alert(exception.message);
        }
        return throwError(err);
      })
    );
  }
}

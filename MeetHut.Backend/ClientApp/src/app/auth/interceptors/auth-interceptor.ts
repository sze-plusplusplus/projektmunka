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
import { mergeMap, catchError } from 'rxjs/operators';
import { AuthService, TokenService } from '../services';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private tokenService: TokenService
  ) {}

  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let req = request;

    // If the token exists, then we add it to the HTTP request
    if (this.authService.accessTokenExists) {
      req = this.cloneRequest(request);
    }

    // Send request and catch Auth errors
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err.status === 401) {
          // Try to refresh the token
          if (this.authService.refreshTokenExists) {
            return this.tokenService.refreshObservable().pipe(
              catchError(() => throwError(err)),
              mergeMap(() =>
                next
                  .handle(this.cloneRequest(request))
                  .pipe(catchError((newError) => throwError(newError)))
              )
            );
          }
          // TODO: Redirect to login
          // TODO: ignore some route

          return throwError(err);
        } else {
          // Throw error
          return throwError(err);
        }
      })
    );
  }

  private cloneRequest(request: HttpRequest<any>): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.accessToken}`
      }
    });
  }
}

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
import { TokenDTO } from '../dtos';
import { AuthService, TokenService } from '../services';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  // TODO: regex
  private ignoredPaths: string[] = [
    'Token/refresh',
    'Auth/login',
    'Auth/ms-login',
    'Auth/google-login',
    'Auth/registration'
  ];

  constructor(
    private authService: AuthService,
    private tokenService: TokenService
  ) {}

  /**
   * Intercept to the HTTP request
   *
   * @param request Request
   * @param next Handler
   * @returns HTTP event
   */
  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Normal mechanism when the path is ignored
    if (this.ignoredPaths.some((path: string) => request.url.endsWith(path))) {
      return next.handle(request);
    }

    let req = request;

    // If the token exists, then we add it to the HTTP request
    if (this.authService.accessTokenExists) {
      req = this.cloneRequest(request);

      // Check token expiration
      if (this.tokenService.tokenIsExpired()) {
        if (this.authService.refreshTokenExists) {
          // Pre refresh if the token is expired and the refresh token is exist
          return this.tokenService.refreshObservable().pipe(
            catchError((err: HttpErrorResponse) => {
              this.notAuthorizedEvent();
              return throwError(err);
            }),
            mergeMap((res: TokenDTO) => {
              this.authService.saveTokens(res);
              req = this.cloneRequest(request);

              // Send the request with the refreshed token
              return this.createRequestStack(next, request, req, false);
            })
          );
        } else {
          // Refresh token is not exist
          this.notAuthorizedEvent();
          return next.handle(request);
        }
      } else {
        // Does not need pre-refresh
        // Send request
        return this.createRequestStack(next, request, req, true);
      }
    }

    // Does not need pre-refresh
    // Send request
    return this.createRequestStack(next, request, req, true);
  }

  private cloneRequest(request: HttpRequest<any>): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.accessToken}`
      }
    });
  }

  private createRequestStack(
    next: HttpHandler,
    originalRequest: HttpRequest<any>,
    request: HttpRequest<any>,
    doRefresh: boolean = false
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        // Access token exists
        if (this.authService.accessTokenExists) {
          // Performed Not Authorized error
          if (err.status === 401) {
            // Try to refresh the token if needed
            if (doRefresh) {
              // Refresh token exists
              if (this.authService.refreshTokenExists) {
                // Refresh tokens
                return this.tokenService.refreshObservable().pipe(
                  catchError(() => {
                    this.notAuthorizedEvent();
                    return throwError(err);
                  }),
                  mergeMap((res: TokenDTO) => {
                    this.authService.saveTokens(res);

                    // Re-send request with the refreshed token
                    return next.handle(this.cloneRequest(originalRequest));
                  })
                );
              } else {
                // Refresh token does not exist
                this.notAuthorizedEvent();
                return throwError(err);
              }
            }

            // Not need to refresh
            this.notAuthorizedEvent();
            return throwError(err);
          } else {
            // Not 401 error
            return throwError(err);
          }
        } else {
          // Access token does not exist
          return throwError(err);
        }
      })
    );
  }

  private notAuthorizedEvent(): void {
    this.authService.clearTokens();
    this.authService.navigateToTheLoginPageWithRoute();
  }
}

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { AuthModule, MediaModule, WebModule } from '.';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    /* Material */
    MaterialModule,

    /* Application modules */
    AuthModule,
    MediaModule,
    WebModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}

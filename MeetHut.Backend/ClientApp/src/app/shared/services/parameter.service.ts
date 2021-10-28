import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ParameterDTO } from '../models/parameter.dto';

@Injectable({
  providedIn: 'root'
})
export class ParameterService {
  constructor(private http: HttpClient) {}

  get(key: string): Promise<ParameterDTO> {
    return this.http.get<ParameterDTO>(this.getParameterUrl(key)).toPromise();
  }

  getAll(): Promise<ParameterDTO[]> {
    return this.http.get<ParameterDTO[]>(this.getParameterUrl('')).toPromise();
  }

  private getParameterUrl(endpoint: string): string {
    return `${environment.apiUrl}/Parameter/${endpoint}`;
  }
}

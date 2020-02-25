import { environment } from './../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Resource } from './../models/resource';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Params } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ResourceService<T extends Resource> {
  constructor(
      private httpClient: HttpClient,
      private endpoint: string) {}
  
    public create(item: T): Observable<T> {
      return this.httpClient
        .post<T>(`${environment.baseApiUrl}/${this.endpoint}/`, item);
    }
  
    public update(item: T): Observable<T> {
      return this.httpClient
        .put<T>(`${environment.baseApiUrl}/${this.endpoint}/${item.id}`, item);
    }
  
    read(id: number): Observable<T> {
      return this.httpClient.get<T>(`${environment.baseOdataUrl}/${this.endpoint}/${id}`);
    }
  
    list(queryOptions: HttpParams): Observable<any> {
      return this.httpClient
        .get<T[]>(`${environment.baseOdataUrl}/${this.endpoint}?${queryOptions == null ? '' : queryOptions}`);
    }
  
    delete(id: number) {
      return this.httpClient
        .delete(`${environment.baseApiUrl}/${this.endpoint}/${id}`);
    }
  }

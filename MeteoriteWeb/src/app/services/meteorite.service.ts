import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MeteoriteFilter } from '../interfaces/meteorite-filter';
import { MeteoriteGroup } from '../interfaces/meteorite-group';

@Injectable({
  providedIn: 'root'
})
export class MeteoriteService {
  private apiUrl = "http://localhost:5139/meteorite";

  constructor(private _httpClient: HttpClient) { }

  public getDateRangeList(): Observable<number[]> {
    return this._httpClient.get<number[]>(`${this.apiUrl}/getYearRangeList`);
  }

  public getRecClassList(): Observable<string[]> {
    return this._httpClient.get<string[]>(`${this.apiUrl}/getRecClassList`);
  }

  public getByFilter(filter: MeteoriteFilter): Observable<MeteoriteGroup[]> {
    return this._httpClient.post<MeteoriteGroup[]>(`${this.apiUrl}/getByFilter`, filter);
  }
}

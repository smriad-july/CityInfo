import { Injectable } from '@angular/core';
import { AddCityRequest } from '../models/add-city-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { City } from '../models/city.model';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  private readonly apiUrl = 'https://localhost:7156/api/cities';
  constructor(private http: HttpClient) { }

  addCity(addCityRequest: AddCityRequest): Observable<AddCityRequest> {
    return this.http.post<AddCityRequest>(this.apiUrl,addCityRequest);
  }

  getAllCities(): Observable<City[]>{
    
      return this.http.get<City[]>(this.apiUrl);
  }

  getCity(id: number): Observable<City>{
    return this.http.get<City>(`${this.apiUrl}/`+id);

  }

  updateCity(id: number, updateCityRequest: City): Observable<City>{
    return this.http.put<City>(`${this.apiUrl}/`+id, updateCityRequest);
  }

  deleteCity(id: number): Observable<City>{
    return this.http.delete<City>(`${this.apiUrl}/`+id);
  }
}

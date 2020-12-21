import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Doctor } from './doctor';
@Injectable({
  providedIn: 'root'
})
export class SharedService {

 
  private baseUrl = 'https://localhost:33786/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
   private http: HttpClient) {}


  /** GET doctor from the server */
  getDoctor(id: number): Observable<Doctor> {
    const url = `${this.baseUrl}/Doctors/1`;
    return this.http.get<Doctor>(url).pipe(
      tap(_ => this.log(`fetched doctor id=1`)),
      catchError(this.handleError<Doctor>(`getDoctor id=1`))
    );
  }
  
  private handleError<T>(operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {

    // TODO: send the error to remote logging infrastructure
    console.error(error); // log to console instead

    // TODO: better job of transforming error for user consumption
    this.log(`${operation} failed: ${error.message}`);

    // Let the app keep running by returning an empty result.
    return of(result as T);
  };
}
private log(message: string) {
  console.log(message);
}

}

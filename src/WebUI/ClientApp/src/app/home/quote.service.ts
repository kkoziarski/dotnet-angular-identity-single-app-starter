import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

const routes = {
  quote: (c: RandomQuoteContext) => `/jokes/random?category=${c.category}`,
  todos: `/todolists/public/list`,
};

export interface RandomQuoteContext {
  // The quote's category: 'dev', 'explicit'...
  category: string;
}

@Injectable({
  providedIn: 'root',
})
export class QuoteService {
  constructor(private httpClient: HttpClient) {}

  getRandomQuote(context: RandomQuoteContext): Observable<string> {
    return this.httpClient.get(routes.quote(context)).pipe(
      map((body: any) => body.value),
      catchError(() => of('Error, could not load joke :-('))
    );
  }

  getTodos(): Observable<any> {
    return this.httpClient.get(routes.todos).pipe(
      map((body: any) => body),
      catchError(() => of('Error, could not load todos'))
    );
  }
}

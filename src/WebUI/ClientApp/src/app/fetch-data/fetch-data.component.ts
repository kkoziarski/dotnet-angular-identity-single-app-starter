import { Component } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { WeatherForecast, WeatherForecastClient } from '../web-api-client';
import { QuoteService } from './quote.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss'],
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public quote: string | undefined;
  public isLoading = false;

  constructor(private client: WeatherForecastClient, private quoteService: QuoteService) {}

  ngOnInit() {
    this.client.get().subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => console.error(error)
    );

    this.isLoading = true;
    this.quoteService
      .getRandomQuote({ category: 'dev' })
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe((quote: string) => {
        this.quote = quote;
      });
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: WeatherForecast[];

  constructor(private http: HttpClient, private authService: AuthService, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    const headers = new HttpHeaders({ 'Authorization': this.authService.getAuthorizationHeaderValue() });

    this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast', { headers: headers }).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
import { Injectable, Type } from '@angular/core';
import {
  HttpHeaders,
  HttpClient,
} from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable()
export class HttpEntityRepositoryService<T> {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}

  upload(_url: string, _content: any): Observable<T> {
    return this.http.post<T>(environment.BASE_URL + '/api/' + _url, _content, {
      reportProgress: true,
      responseType: 'json'
    });
  }
  
  delete(_url: string, content: any): Observable<T> {
    debugger;
    return this.http.post<T>(
      environment.BASE_URL + '/api/' + _url, content,
      this.httpOptions
    );
  }
}

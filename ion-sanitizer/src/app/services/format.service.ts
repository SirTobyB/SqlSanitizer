import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {FormatRequest} from '../models/format-request';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormatService {

  constructor(private http: HttpClient) { }

  formatSql(formatRequest: FormatRequest) {
    return this.http.post(environment.apiUrl, formatRequest);
  }
}

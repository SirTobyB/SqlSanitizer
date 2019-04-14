import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { FormatResponse } from '../FormatResponse';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  sqlQuery: string;
  charsToRemove = '';
  formattedSql: string;

  constructor(private http: HttpClient) { }

  format() {

  var charsToRemoveSplits = new Array();

  if (this.charsToRemove !== ''){
    charsToRemoveSplits = this.charsToRemove.replace(' ', '').split(',');
  }

  const body = {
    sqlQuery: this.sqlQuery,
    charsToRemove: charsToRemoveSplits
  };

    this.http.post<FormatResponse>(environment.apiUrl, body)
      .subscribe(response => this.formattedSql = response.sql);
  }

  ngOnInit() {}
}

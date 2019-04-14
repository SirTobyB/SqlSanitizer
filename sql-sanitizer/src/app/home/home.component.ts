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
  reindent = true;
  indentWidth = 2;
  identifierCase = 'Default';
  keywordCase = 'Default';
  stripComments = false;

  casingOptions = ['Default', 'Upper', 'Lower', 'Capitalize'];
  formattedSql = '';

  constructor(private http: HttpClient) { }

  format() {

  let charsToRemoveSplits = new Array();

  if (this.charsToRemove !== ''){
    charsToRemoveSplits = this.charsToRemove.replace(' ', '').split(',');
  }

  const body = {
    sqlQuery: this.sqlQuery,
    charsToRemove: charsToRemoveSplits,
    reindent: this.reindent,
    indentWidth: this.indentWidth,
    identifierCase: this.identifierCase,
    keywordCase: this.keywordCase,
    stripComments: this.stripComments
  };

  console.log(body);

  this.http.post<FormatResponse>(environment.apiUrl, body)
      .subscribe(response => this.formattedSql = response.sql);
  }

  ngOnInit() {}
}

import {Component, OnInit} from '@angular/core';
import {GithubService} from '../services/github.service';
import {Observable} from 'rxjs';
import {Contributor} from '../models/contributor';
import {SqlParameter} from '../models/sql-parameter';
import * as _ from 'lodash';
import {FormatRequest} from '../models/format-request';
import {FormatService} from '../services/format.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit{
  parameterRegex = /\@[A-Za-z0-9]*/g;

  editorOptions = {
    theme: 'vs-dark',
    language: 'sql',
    wordWrap: 'on',
    automaticLayout: true,
    scrollBeyondLastLine: false,
  };

  casingOptions = ['Default', 'Upper', 'Lower', 'Capitalize'];

  formatOptions: FormatRequest = {
    charsToRemove: [],
    reindent: true,
    indentWidth: 2,
    identifierCase: 'Default',
    keywordCase: 'Default',
    stripComments: false,
    sqlQuery: '',
    parameter: []
  };

  charsToRemove = '';

  contributors$: Observable<Contributor[]>;

  constructor(private github: GithubService, private formatService: FormatService) {
  }

  ngOnInit(): void {
    this.contributors$ = this.github.getContributors();
    this.formatOptions.sqlQuery = 'SELECT * FROM users;';
    this.updateParams();
  }

  onModelChange() {
    this.updateParams();
  }

  formatQuery() {
    this.formatOptions.charsToRemove = this.charsToRemove.replace(' ', '').split(',');
    console.log(this.formatOptions);
    this.formatService.formatSql(this.formatOptions)
        .subscribe(resp => this.formatOptions.sqlQuery = resp['sql']);
  }

  updateParams() {
    let m: RegExpExecArray;
    const foundParams: SqlParameter[] = [];

    do {
      m = this.parameterRegex.exec(this.formatOptions.sqlQuery);

      if (m) {
        if (foundParams.find(param => param.name === m[0])) {
          continue;
        }
        const existingParam = _.find(this.formatOptions.parameter, param => param.name === m[0]);
        const paramValue = existingParam ? existingParam.value : '';

        foundParams.push(new SqlParameter(m[0], paramValue));
      }
    }
    while (m);

    this.formatOptions.parameter = foundParams;
  }
}

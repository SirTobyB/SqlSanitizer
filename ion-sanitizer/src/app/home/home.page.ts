import {Component, OnInit} from '@angular/core';
import {GithubService} from '../services/github.service';
import {Observable} from 'rxjs';
import {Contributor} from '../models/contributor';
import {SqlParameter} from '../models/sql-parameter';
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

  parameter: SqlParameter[] = [];

  code = 'SELECT * FROM table WHERE column = @value';
  contributors$: Observable<Contributor[]>;

  constructor(private github: GithubService) {
  }

  ngOnInit(): void {
    this.contributors$ = this.github.getContributors();
  }

  onModelChange() {
    let m: RegExpExecArray;

    do {
      m = this.parameterRegex.exec(this.code);

      if (m && !this.parameter.find(param => param.name === m[0])) {
        this.parameter.push(new SqlParameter(m[0]));
      }
    }
    while (m);
  }

}

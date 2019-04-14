import { Component, OnInit, Input } from '@angular/core';
import { SqlParameter } from '../models/SqlParameter';

@Component({
  selector: 'app-sql-parameter',
  templateUrl: './sql-parameter.component.html',
  styleUrls: ['./sql-parameter.component.scss']
})
export class SqlParameterComponent implements OnInit {

  @Input()
  parameter: SqlParameter;

  constructor() { }

  ngOnInit() {
  }

}

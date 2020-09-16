import {Component, OnInit} from '@angular/core';
import {GithubService} from '../services/github.service';
import {Observable} from 'rxjs';
import {Contributor} from '../models/contributor';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit{

  contributors$: Observable<Contributor[]>;

  constructor(private github: GithubService) {
  }

  ngOnInit(): void {
    this.contributors$ = this.github.getContributors();
  }

}

import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Contributor} from '../models/contributor';

@Injectable({
  providedIn: 'root'
})
export class GithubService {

  constructor(private http: HttpClient) { }

  getContributors() {
    return this.http.get<Contributor[]>(environment.contributorsEndpoint);
  }
}

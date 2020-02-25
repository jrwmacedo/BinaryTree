import { environment } from './../../../environments/environment';
import { Player } from './../../models/player/player';
import { HttpClient } from '@angular/common/http';
import { ResourceService } from './../resource.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlayerService extends ResourceService<Player> {
  constructor(httpClient: HttpClient){
    super(httpClient, environment.playerEndPoint)
  }
}

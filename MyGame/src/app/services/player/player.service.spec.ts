import { Player } from './../../models/player/player';
import { of } from 'rxjs';
import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PlayerService } from './player.service';
import { HttpClient } from '@angular/common/http';

describe('PlayerService', () => {
  let service: PlayerService;
  beforeEach(() => TestBed.configureTestingModule({
    providers: [PlayerService,
      {
        provide: HttpClient,
        useValue: {
          get: () => of({}),
          post: () => of({})
        }
      }]
  }));

  it('should be created', () => {
    service = TestBed.get(PlayerService);
    expect(service).toBeTruthy();
  });

  it('should use GET from httpClient when reading', () => {
    service = TestBed.get(PlayerService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'get');
    service.read(1);
    expect(client.get).toHaveBeenCalled();
  })

  it('should use POST from httpClient when creating', () => {
    let player = new Player();
    service = TestBed.get(PlayerService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'post');
    service.create(player);
    expect(client.post).toHaveBeenCalled();
  })
});

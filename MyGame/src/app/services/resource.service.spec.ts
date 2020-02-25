import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { ResourceService } from './resource.service';
import { Player } from '../models/player/player';
import { HttpClient } from '@angular/common/http';

describe('ResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [ResourceService],
    imports: [HttpClientTestingModule]
  }));

  it('should be created', () => {
    // const service: ResourceService<Player> = TestBed.get(ResourceService);
    const httpClient = TestBed.get(HttpClient);
    const service = new ResourceService<Player>(httpClient, 'abc');
    expect(service).toBeTruthy();
  });
});

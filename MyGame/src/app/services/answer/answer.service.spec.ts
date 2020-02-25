import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AnswerService } from './answer.service';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { Answer } from 'src/app/models/answer/answer';

describe('AnswerService', () => {
  let service: AnswerService;
  beforeEach(() => TestBed.configureTestingModule({
    providers: [AnswerService,
      {
        provide: HttpClient,
        useValue: {
          get: () => of({}),
          post: () => of({})
        }
      }]
  }));

  it('should be created', () => {
    service = TestBed.get(AnswerService);
    expect(service).toBeTruthy();
  });

  it('should use GET from httpClient when reading', () => {
    service = TestBed.get(AnswerService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'get');
    service.read(1);
    expect(client.get).toHaveBeenCalled();
  })

  it('should use POST from httpClient when creating', () => {
    let answer = new Answer();
    service = TestBed.get(AnswerService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'post');
    service.create(answer);
    expect(client.post).toHaveBeenCalled();
  })

});

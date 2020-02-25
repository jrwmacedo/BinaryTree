import { Question } from './../../models/question/question';
import { of } from 'rxjs';
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { QuestionService } from './question.service';
import { HttpClient } from '@angular/common/http';

describe('QuestionService', () => {
  let service: QuestionService;
  beforeEach(() => TestBed.configureTestingModule({
    providers: [QuestionService,
      {
        provide: HttpClient,
        useValue: {
          get: () => of({}),
          post: () => of({})
        }
      }]
  }));

  it('should be created', () => {
    service = TestBed.get(QuestionService);
    expect(service).toBeTruthy();
  });

  it('should use GET from httpClient when reading', () => {
    service = TestBed.get(QuestionService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'get');
    service.read(1);
    expect(client.get).toHaveBeenCalled();
  })

  it('should use POST from httpClient when creating', () => {
    let question = new Question();
    service = TestBed.get(QuestionService);
    const client: HttpClient = TestBed.get(HttpClient);
    spyOn(client, 'post');
    service.create(question);
    expect(client.post).toHaveBeenCalled();
  })
});

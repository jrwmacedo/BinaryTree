import { environment } from './../../../environments/environment';
import { Question } from '../../models/question/question';
import { HttpClient } from '@angular/common/http';
import { ResourceService } from './../resource.service';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class QuestionService extends ResourceService<Question> {
  constructor(httpClient: HttpClient){
    super(httpClient, environment.questionEndPoint)
  }
}

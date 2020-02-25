import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ResourceService } from './../resource.service';
import { Answer } from './../../models/answer/answer';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AnswerService extends ResourceService<Answer> {
  constructor(httpClient: HttpClient){
    super(httpClient, environment.questionEndPoint)
  }


}

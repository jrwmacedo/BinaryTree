import { AnswerService } from './../../services/answer/answer.service';
import { Question } from '../../models/question/question';
import { QuestionService } from './../../services/question/question.service';
import { Component, OnInit } from '@angular/core';
import { Answer } from 'src/app/models/answer/answer';
import { Player } from 'src/app/models/player/player';

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.scss']
})
export class TreeComponent implements OnInit {
  public questions: Question;
  public selectedTreeNode = new Map<number, number>()
  public showAllQuestion: boolean = false;
  public highlightChoices: boolean = true;
  public player: Player;
  public message: String;
  public isLoaded: boolean = false;
  constructor(
    private questionService: QuestionService,
    private answerService: AnswerService
  ) { }

  receivePlayer($event) {
    this.player = $event;
    if (!!this.player.playerQuestions) {
      this.player.playerQuestions.map(pq => this.selectedTreeNode.set(pq.parentId, pq.childId));
    }
    this.questions.showDescription = true;
  }

  receiveMessage($event) {
    this.message = $event
  }

  ngOnInit() {
    this.questionService.read(1).subscribe(response => {
      this.questions = response;
      this.selectedTreeNode.set(0, this.questions.id);
      this.questions.showDescription = true;
    });
  }

  public selectNode(node: Question): void {

    if (!!node && !this.selectedTreeNode.has(node.parentId) && !this.showAllQuestion) {
      this.selectedTreeNode.set(node.parentId, node.id);
      node.showDescription = true;
      if (!node.children) {
        let answer: Answer = new Answer();
        answer.id = this.player.id;
        for (let value of this.selectedTreeNode.values()) {
          answer.Answers.push(value)
        }
        this.answerService.create(answer).subscribe();
      }
    }
  }

  private findNode(id: number) {

    let nodeFound: boolean = false;
    if (this.selectedTreeNode.size > 0) {
      this.selectedTreeNode.forEach(function (value, key) {
        if (value == id) {
          nodeFound = true;
          return;
        }
      })
    }
    return nodeFound;
  }

  public showNode(node: Question) {
    if (!node || !node.parentId || this.showAllQuestion) {
      return true;
    } else {
      return this.findNode(node.parentId);
    }
  }

  public showYesNo(node: Question) {
    return node.yesNo;
  }

  public manageYesNo(yesNo: boolean) {
    return yesNo ? "Yes" : "No";
  }

  public checkHighlight(node: Question) {
    return this.highlightChoices && (!!node && this.findNode(node.id));
  }

  public manageDescription(node: Question) {
    return !node || node.showDescription || this.showAllQuestion || this.findNode(node.id);
  }
}

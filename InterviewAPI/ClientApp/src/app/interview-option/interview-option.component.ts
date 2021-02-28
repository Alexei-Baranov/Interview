import {Component, Input, OnInit} from '@angular/core';
import {AnswerRequest, InterviewAnswerService} from '../shared/interview.answer.service';
import {InterviewOptionService, OptionResponse} from '../shared/interview.option.service';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

export interface Option {
  id: number;
  title: string;
  selected: boolean;
}


@Component({
  selector: 'app-interview-option',
  templateUrl: './interview-option.component.html',
})
export class InterviewOptionComponent implements OnInit {

  @Input() optionResponses: OptionResponse[];
  @Input() maxSelectedOptionCount: number;
  @Input() interviewId: number;
  @Input() allOptionRequired: boolean;

  options: Option[];
  answerRequest: AnswerRequest;
  answered: boolean;

  constructor(private answerService: InterviewAnswerService, private optionService: InterviewOptionService) {
    this.answerRequest = {optionsId : []};
    this.options = [];
  }

  postAnswer(optionId: number) {
    this.answerRequest.optionsId = [];
    this.answerRequest.optionsId.push(optionId);
    console.log(this.answerRequest);
    this.answerService.answerTheInterview(this.interviewId, this.answerRequest).subscribe(value => {
      this.answered = true;
      this.optionService.getOptions(this.interviewId);
      console.log(value);
    }, error => {
      console.log(error);
    });

  }

  postAnswers() {
    this.answerRequest.optionsId = [];
    this.options.filter(value => value.selected === true).map(value => {
      this.answerRequest.optionsId.push(value.id);
    });
    console.log(this.answerRequest);
    this.answerService.answerTheInterview(this.interviewId, this.answerRequest).subscribe(value => {
      console.log(value);
      this.answered = true;
      this.optionService.getOptions(this.interviewId).subscribe(value1 => this.optionResponses = value1);
    }, error => {
      console.log(error);
    });
  }

  getOptions() {
    this.optionService.getOptions(this.interviewId);
  }

  ngOnInit(): void {
    this.optionResponses.map(value => this.options.push({id: value.id, title: value.title, selected: false}));
  }
}

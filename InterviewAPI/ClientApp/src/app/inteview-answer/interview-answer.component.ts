import {Component, Input, OnInit} from '@angular/core';
import {InterviewAnswerService} from '../shared/interview.answer.service';
import {InterviewService} from '../shared/interview.service';

@Component({
  selector: 'app-interview-answer',
  templateUrl: './interview-answer.component.html',
})
export class InterviewAnswerComponent implements OnInit {
  constructor(public answerService: InterviewAnswerService, public interviewService: InterviewService) {
  }

  ngOnInit(): void {
    this.getNextInterview();
  }

  getNextInterview(): void {
    this.interviewService.getInterviewForAnswer(1, 1).subscribe(value => console.log(value));
  }

}

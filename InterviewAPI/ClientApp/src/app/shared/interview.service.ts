import {Injectable} from '@angular/core';
import {Time} from '@angular/common';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {OptionResponse} from './interview.option.service';

export interface InterviewRequest {
  question: string;
  allOptionRequired: boolean;
  interviewStart: Time;
  interviewEnd?: any;
  responsesCountForPublish: number;
  responsesIsPublic: boolean;
  isPublished: boolean;
}

export interface InterviewResponse {
  id: number;
  creationTimestamp: Time;
  question: string;
  options: OptionResponse[];
  allOptionRequired: boolean;
  maxSelectedOptionCount: number;
  interviewStart: Time;
  interviewEnd: any;
  responsesCountForPublish: number;
  responsesIsPublic: boolean;
  isPublished: boolean;
  createdUserId: string;
}

export enum InterviewType {
  ForUser,
  ForAnswer,
}


@Injectable({providedIn: 'root'})
export class InterviewService {
  constructor(private interviewClient: HttpClient) { }

  public interviews: InterviewResponse[] = [];

  public interviewForAnswer: InterviewResponse;

  fetchInterviews(pageIndex: number, pageSize: number): Observable<InterviewResponse[]> {
    return this.interviewClient.get<InterviewResponse[]>(`https://localhost:5001/interview?pageIndex=${ pageIndex }&pageSize=${ pageSize }&type=${ InterviewType.ForUser }`)
      .pipe(tap(x => this.interviews = x));
  }

  createInterview(interviewRequest: InterviewRequest) {
    return this.interviewClient.post('https://localhost:5001/interview', interviewRequest);
  }

  getInterviewForAnswer(pageIndex: number, pageSize: number): Observable<InterviewResponse[]> {
    return this.interviewClient.get<InterviewResponse[]>(`https://localhost:5001/interview?pageIndex=${ pageIndex }&pageSize=${ pageSize }&type=${ InterviewType.ForAnswer }`)
      .pipe(tap(x => this.interviewForAnswer = x.pop()));
  }

}

import {Injectable} from '@angular/core';
import {Time} from '@angular/common';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';

export interface AnswerRequest {
  optionsId: number[];
}

export interface AnswerResponse {
  id: number;
  creationTimestamp: Time;
  interviewId: number;
  optionId: number;
  userId: string;
}

@Injectable({providedIn: 'root'})
export class InterviewAnswerService {
  constructor(private client: HttpClient) { }

  public answerResponses: AnswerResponse[] = [];



  fetchAnswers(pageIndex: number, pageSize: number): Observable<AnswerResponse[]> {
    return this.client.get<AnswerResponse[]>(`https://localhost:5001/interview/answer?pageIndex=${ pageIndex }&pageSize=${ pageSize }`)
      .pipe(tap(x => this.answerResponses = x));
  }

  answerTheInterview(interviewId: number, interviewRequest: AnswerRequest): Observable<number> {
    return this.client.post<number>(`https://localhost:5001/interview/answer?interviewId=${interviewId}`, interviewRequest);
  }
}

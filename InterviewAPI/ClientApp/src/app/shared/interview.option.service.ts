import {Injectable} from '@angular/core';
import {Time} from '@angular/common';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';

export interface OptionRequest {
  title: String;
}

export interface OptionResponse {
  id: number;
  title: string;
  answerCount: number;
  selected: boolean;
}

@Injectable({providedIn: 'root'})
export class InterviewOptionService {
  constructor(private client: HttpClient) { }

  public optionResponses: OptionResponse[] = [];

  getOptions(interviewId: number): Observable<OptionResponse[]> {
    return this.client.get<OptionResponse[]>(`https://localhost:5001/interview/option?interviewId=${interviewId}`)
      .pipe(tap(x => this.optionResponses = x));
  }

  AddOptionForInterview(interviewId: number, optionRequest: OptionRequest): Observable<OptionResponse> {
    return this.client.post<OptionResponse>(`https://localhost:5001/interview?id=${interviewId}`, optionRequest);
  }



}

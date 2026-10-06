import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class QuizService {
    private readonly apiUrl = 'https://localhost:7179/api/Quizs';

    constructor(private http: HttpClient) { }

    getQuiz() {
        return this.http.get(`${this.apiUrl}`);
    }

    getQuizDetail(id: number) {
        return this.http.get(`${this.apiUrl}/${id}`);
    }

    createQuiz(quiz: any) {
        return this.http.post(`${this.apiUrl}`, quiz);
    }

    updateQuiz(quiz: any) {
        return this.http.patch(`${this.apiUrl}`, quiz);
    }

    getQuizQuestions(id: number) {
        return this.http.get(`${this.apiUrl}/${id}/Questions`);
    }

    checkResult(data: any) {
        return this.http.post(`${this.apiUrl}/CheckResults`, data);
    }
}
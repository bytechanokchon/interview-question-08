import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { QuizDto } from '../shareds/models/quiz.model';
import { QuizService } from '../cores/services/quiz.service';

@Component({
  imports: [],
  selector: 'app-quiz-list',
  styleUrl: './quiz-list.css',
  templateUrl: './quiz-list.html',
})
export class QuizList {
  quizs: QuizDto[] = [];

  constructor(public router: Router, private quizService: QuizService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.quizService.getQuiz().subscribe((data: any) => {
      if (data.isSuccess) {
        const quizTemps: QuizDto[] = [];

        for (const quiz of data.value) {
          quizTemps.push({
            clientId: crypto.randomUUID(),
            id: quiz.id,
            title: quiz.title,
          });
        }
        
        this.quizs = [...quizTemps];
        this.cdr.detectChanges();
      }
    });
  }

  goToDetail(id: number): void {
    this.router.navigate(['/quiz', id]);
  }
}

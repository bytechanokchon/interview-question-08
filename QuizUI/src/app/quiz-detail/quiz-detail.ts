import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionInput } from '../components/question-input/question-input';
import { QuestionDto } from '../shareds/models/question.model';
import { RowStateEnum } from '../shareds/enums/rowState.enum';
import { QuizService } from '../cores/services/quiz.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  imports: [QuestionInput, ReactiveFormsModule],
  selector: 'app-quiz-detail',
  styleUrl: './quiz-detail.css',
  templateUrl: './quiz-detail.html',
})
export class QuizDetail {
  form!: FormGroup;
  quizId: number | null = null;
  questions: QuestionDto[] = [];
  questionDeletes: QuestionDto[] = [];

  constructor(private route: ActivatedRoute, private quizService: QuizService, private fb: FormBuilder, public router: Router) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.quizId = id ? Number(id) : null;

    this.initialForm();

    if (this.quizId) {
      this.quizService.getQuizDetail(this.quizId).subscribe((data: any) => {
        if (data.isSuccess && data.value) { 
        
          this.form.patchValue({
            id: data.value.id,
            questionTitle: data.value.title,
          });

          for (const question of data.value.questions) {
            const questionDto: QuestionDto = {
              clientId: crypto.randomUUID(),
              id: question.id,
              title: question.title,
              resultOptions: question.resultOptions.map((option: any) => ({
                id: option.id,
                title: option.title,
                isCorrect: option.isCorrect,
                rowState: RowStateEnum.NORMAL
              })),
              rowState: RowStateEnum.NORMAL
            };

            this.questions.push(questionDto);
          }
        
        }
      });
    }
  }

  initialForm() {
    this.form = this.fb.group({
      id: [this.quizId],
      questionTitle: ['', Validators.required],
    });
  }

  onAddQuestion() {
    const newQuestion: QuestionDto = {
      clientId: crypto.randomUUID(),
      id: null,
      title: '',
      resultOptions: [
        { id: null, title: '', isCorrect: false, rowState: RowStateEnum.CREATE },
        { id: null, title: '', isCorrect: false, rowState: RowStateEnum.CREATE },
        { id: null, title: '', isCorrect: false, rowState: RowStateEnum.CREATE },
        { id: null, title: '', isCorrect: false, rowState: RowStateEnum.CREATE },
      ],
      rowState: RowStateEnum.CREATE
    };

    this.questions.push(newQuestion);
  }

  onDeleteQuestion(index: number) {
    const questionTemps: QuestionDto[] = []

    debugger;

    for (let i = 0; i < this.questions.length; i++) {
      const question = this.questions[i];

      if (i === index) {
        if (question.id) {
          question.rowState = RowStateEnum.DELETE;
          question.resultOptions.forEach((option) => {
            option.rowState = RowStateEnum.DELETE;
          });

          this.questionDeletes.push(question);
          continue;
        }
      }

      questionTemps.push(question);
    }

    this.questions = questionTemps;
  }

  onQuestionChange(updatedQuestion: QuestionDto, index: number) {
    if (index >= 0 && index < this.questions.length) {
      updatedQuestion.rowState = updatedQuestion.id ? RowStateEnum.UPDATE : RowStateEnum.CREATE;
      updatedQuestion.resultOptions.forEach((option) => {
        option.rowState = option.id ? RowStateEnum.UPDATE : RowStateEnum.CREATE;
      });

      this.questions[index] = updatedQuestion;
    }
  }

  onSubmit() {
    const questionsToSubmit = this.questions;
    const questionsToDelete = this.questionDeletes;

    if (this.quizId) {
      // call update quiz API with this.quizId, questionsToSubmit, and questionsToDelete
      const questions = [...questionsToSubmit, ...questionsToDelete];

      const dataToSubmit = {
        id: this.quizId,
        title: this.form.value.questionTitle,
        Questions: questions.map((question) => ({
          id: question.id,
          title: question.title,
          rowState: question.rowState,
          ResultOptions: question.resultOptions.map((option) => ({
            id: option.id,
            title: option.title,
            isCorrect: option.isCorrect,
            rowState: option.rowState,
          })),
        }))
      };

      this.quizService.updateQuiz(dataToSubmit).subscribe((response) => {
        this.router.navigate(['']);
      });
    } else {
      // call create quiz API with questionsToSubmit
      const dataToSubmit = {
        title: this.form.value.questionTitle,
        questionRequestDtos: questionsToSubmit.map((question) => ({
          title: question.title,
          resultOptionRequestDtos: question.resultOptions.map((option) => ({
            title: option.title,
            isCorrect: option.isCorrect,
          })),
        })),
      };

      this.quizService.createQuiz(dataToSubmit).subscribe((response) => {
        this.router.navigate(['']);
      });
    }
  }

  startQuiz() {
    this.router.navigate([`/quiz/${this.quizId}/test`]);
  }
}

import { ChangeDetectorRef, Component } from '@angular/core';
import { QuestionInput } from '../components/question-input/question-input';
import { QuestionDto } from '../shareds/models/question.model';
import { QuizService } from '../cores/services/quiz.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RowStateEnum } from '../shareds/enums/rowState.enum';

@Component({
  imports: [QuestionInput, ReactiveFormsModule],
  selector: 'app-quiz-test',
  styleUrl: './quiz-test.css',
  templateUrl: './quiz-test.html',
})
export class QuizTest {
  quizId: number | null = null;
  questions: QuestionDto[] = [];
  form!: FormGroup;
  fullScore: number | null = null;
  scoreObtain: number | null = null;

  constructor(
    private route: ActivatedRoute, 
    private quizService: QuizService, 
    private fb: FormBuilder, 
    public router: Router,
    private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.quizId = id ? Number(id) : null;

    this.initialForm();

    if (this.quizId) {
      this.quizService.getQuizQuestions(this.quizId).subscribe((data: any) => {
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
                isCorrect: null,
                rowState: RowStateEnum.NORMAL
              })),
              rowState: RowStateEnum.NORMAL
            };

            this.questions.push(questionDto);
          }

          this.cdr.detectChanges();
        }
      })
    }

    this.form.disable();
    
  }

  initialForm() {
    this.form = this.fb.group({
      id: [this.quizId],
      questionTitle: ['', Validators.required],
    });
  }

  onQuestionChange(updatedQuestion: QuestionDto, index: number) {
    this.fullScore = null;
    this.scoreObtain = null;

    if (index >= 0 && index < this.questions.length) {
      updatedQuestion.rowState = updatedQuestion.id ? RowStateEnum.UPDATE : RowStateEnum.CREATE;
      updatedQuestion.resultOptions.forEach((option) => {
        option.rowState = option.id ? RowStateEnum.UPDATE : RowStateEnum.CREATE;
      });

      this.questions[index] = updatedQuestion;
    }
  }

  onSubmit() {
    const userResultOptionSelected: any[] = [];

    for (let question of this.questions) {
      let resultOptionIdSelected: number | null | undefined = null;

      for (let resultOption of question.resultOptions) {
        if (resultOption.isCorrect) {
          resultOptionIdSelected = resultOption.id;
          break;
        }
      }

      if (resultOptionIdSelected) {
        userResultOptionSelected.push({
          QuestionId: question.id,
          ResultOptionId: resultOptionIdSelected
        });
      }
    }

    this.quizService.checkResult({
      quizId: this.quizId,
      questions: userResultOptionSelected
    }).subscribe((data: any) => {
      if (data.isSuccess) {
        this.fullScore = data.value.fullScore;
        this.scoreObtain = data.value.scoreObtained;
        this.cdr.detectChanges();

        console.log(this.fullScore)
        console.log(this.scoreObtain)
      }
    })
  }
}

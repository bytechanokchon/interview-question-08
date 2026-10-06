import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { QuestionDto } from '../../shareds/models/question.model';
import { RowStateEnum } from '../../shareds/enums/rowState.enum';

@Component({
  imports: [
    ReactiveFormsModule
  ],
  selector: 'app-question-input',
  styleUrl: './question-input.css',
  templateUrl: './question-input.html',
})
export class QuestionInput {
  @Input() index: number = 0;
  @Input() question!: QuestionDto;
  @Input() isDisabled: boolean = false;

  @Output() deleteQuestion = new EventEmitter<number>();
  @Output() questionChange = new EventEmitter<QuestionDto>();

  form!: FormGroup;
  radioInputName: string = '';

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.radioInputName = `question-${this.index}`;

    this.intialForm();

    let correctChoice: string | null = null;
    for (let i = 0; i < 4; i++) {
      if (this.question?.resultOptions[i].isCorrect) {
        correctChoice = i.toString();
        break;
      }
    }

    this.form.patchValue({
      correctChoice: correctChoice,
      questionTitle: this.question?.title || null,
      resultOptionOne: this.question?.resultOptions[0]?.title || null,
      resultOptionTwo: this.question?.resultOptions[1]?.title || null,
      resultOptionThree: this.question?.resultOptions[2]?.title || null,
      resultOptionFour: this.question?.resultOptions[3]?.title || null,
    });

    if (this.isDisabled) {
      this.form.disable();
      this.form.get("correctChoice")?.enable();
    }

    this.valueChanges();
  }

  intialForm() {
    this.form = this.fb.group({
      correctChoice: [null, Validators.required],
      questionTitle: [null, Validators.required],
      resultOptionOne: [null, Validators.required],
      resultOptionTwo: [null, Validators.required],
      resultOptionThree: [null, Validators.required],
      resultOptionFour: [null, Validators.required],
    });
  }

  valueChanges() {
    this.form.valueChanges.subscribe((value) => {
      this.questionChange.emit({
        clientId: this.question?.clientId || null,
        id: this.question?.id || null,
        title: value.questionTitle,
        resultOptions: [
          { id: this.question?.resultOptions[0]?.id || null, title: value.resultOptionOne, isCorrect: value.correctChoice === '0', rowState: this.question?.resultOptions[0]?.rowState },
          { id: this.question?.resultOptions[1]?.id || null, title: value.resultOptionTwo, isCorrect: value.correctChoice === '1', rowState: this.question?.resultOptions[1]?.rowState },
          { id: this.question?.resultOptions[2]?.id || null, title: value.resultOptionThree, isCorrect: value.correctChoice === '2', rowState: this.question?.resultOptions[2]?.rowState },
          { id: this.question?.resultOptions[3]?.id || null, title: value.resultOptionFour, isCorrect: value.correctChoice === '3', rowState: this.question?.resultOptions[3]?.rowState },
        ],
        rowState: this.question?.rowState
      });
    });
  }

  onDeleteQuestion() {
    this.deleteQuestion.emit(this.index);
  }
}

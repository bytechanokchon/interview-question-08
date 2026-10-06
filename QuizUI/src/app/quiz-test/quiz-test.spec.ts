import { ComponentFixture, TestBed } from '@angular/core/testing';
import { QuizTest } from './quiz-test';

describe('QuizTest', () => {
  let component: QuizTest;
  let fixture: ComponentFixture<QuizTest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizTest],
    }).compileComponents();

    fixture = TestBed.createComponent(QuizTest);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

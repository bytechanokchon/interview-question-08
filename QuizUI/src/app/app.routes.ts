import { Routes } from '@angular/router';
import { QuizDetail } from './quiz-detail/quiz-detail';
import { QuizList } from './quiz-list/quiz-list';
import { QuizTest } from './quiz-test/quiz-test';

export const routes: Routes = [
    {
        path: '',
        component: QuizList
    },
    {
        path: 'quiz',
        component: QuizDetail
    },
    {
        path: 'quiz/:id',
        component: QuizDetail
    },
    {
        path: 'quiz/:id/test',
        component: QuizTest
    }
];

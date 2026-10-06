import { QuestionDto } from "./question.model";

export interface QuizDto {
    clientId?: string | null;

    id: number;
    title: string;
    questions?: QuestionDto[];
}
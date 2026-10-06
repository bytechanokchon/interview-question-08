import { ResultOptionDto } from "./resultOption.model";

export interface QuestionDto {
    clientId?: string | null;
    id?: number | null;
    title: string;
    resultOptions: ResultOptionDto[];
    rowState: number;
}
import type { QuestionObject } from "./questions";

interface QuizResponse {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  title: string;
  description: string | null;
  thumbnailUrl: string | null;
  creatorId: string;
  creatorName: string;
  isPublic: boolean;
  languageCode: string;
  questionCount: number;
  attemptCount: number;
  topics: string[];
}

interface UpdateQuizRequest {
  title: string | null;
  description: string | null;
  thumbnailUrl: string | null;
  isPublic: boolean | null;
  languageCode: string | null;
}

interface CreateQuizRequest {
  title: string;
  description: string | null;
  thumbnailUrl: string | null;
  isPublic: boolean;
  languageCode: string;
  topics: string[];
  questions: QuestionObject[];
}

export type { QuizResponse, UpdateQuizRequest, CreateQuizRequest };

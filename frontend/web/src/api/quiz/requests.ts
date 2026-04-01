import api from "../apiClient";
import type {
  CreateQuizRequest,
  QuizQuestionsResponse,
  QuizResponse,
  UpdateQuizRequest,
} from "./types";

async function getQuizzes(): Promise<QuizResponse[]> {
  const response = await api.get<QuizResponse[]>("/quizzes");
  return response.data;
}

async function getQuiz(quizId: string): Promise<QuizResponse> {
  const response = await api.get<QuizResponse>(`/quizzes/${quizId}`);
  return response.data;
}

async function createQuiz(request: CreateQuizRequest): Promise<QuizResponse> {
  const response = await api.post<QuizResponse>("/quizzes", request);
  return response.data;
}

async function updateQuiz(quizId: string, request: UpdateQuizRequest) {
  return await api.patch(`/quizzes/${quizId}`, request);
}

async function deleteQuiz(quizId: string) {
  return await api.delete(`/quizzes/${quizId}`);
}

async function getQuizQuestions(
  quizId: string
): Promise<QuizQuestionsResponse> {
  const response = await api.get<QuizQuestionsResponse>(
    `/quizzes/${quizId}/questions`
  );
  return response.data;
}

export {
  getQuizzes,
  getQuiz,
  createQuiz,
  updateQuiz,
  deleteQuiz,
  getQuizQuestions,
};

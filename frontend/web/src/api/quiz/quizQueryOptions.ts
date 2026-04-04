import { queryOptions } from "@tanstack/react-query";

import { getQuiz, getQuizQuestions, getQuizzes } from "./requests";

const quizzesQueryKey = (page: number, pageSize: number) =>
  ["quizzes", page, pageSize] as const;
const quizQueryKey = (quizId: string) => ["quiz", quizId] as const;
const quizQuestionsQueryKey = (quizId: string) =>
  ["quiz", quizId, "questions"] as const;

function getQuizzesQueryOptions(page: number, pageSize: number) {
  return queryOptions({
    queryKey: quizzesQueryKey(page, pageSize),
    queryFn: () =>
      getQuizzes({
        page,
        pageSize,
      }),
    staleTime: 1 * 60 * 1000,
    retry: false,
  });
}

function getQuizQueryOptions(quizId: string) {
  return queryOptions({
    queryKey: quizQueryKey(quizId),
    queryFn: () => getQuiz(quizId),
    staleTime: 1 * 60 * 1000,
    retry: false,
  });
}

function getQuizQuestionsQueryOptions(quizId: string) {
  return queryOptions({
    queryKey: quizQuestionsQueryKey(quizId),
    queryFn: () => getQuizQuestions(quizId),
    staleTime: 1 * 60 * 1000,
    retry: false,
  });
}

export {
  getQuizzesQueryOptions,
  getQuizQueryOptions,
  getQuizQuestionsQueryOptions,
  quizzesQueryKey,
  quizQueryKey,
  quizQuestionsQueryKey,
};

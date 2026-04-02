import { queryOptions } from "@tanstack/react-query";

import { getQuiz, getQuizQuestions } from "./requests";

const quizQueryKey = (quizId: string) => ["quiz", quizId] as const;
const quizQuestionsQueryKey = (quizId: string) =>
  ["quiz", quizId, "questions"] as const;

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
  getQuizQueryOptions,
  getQuizQuestionsQueryOptions,
  quizQueryKey,
  quizQuestionsQueryKey,
};

import { createFileRoute } from "@tanstack/react-router";

import CreateQuizPage from "@/features/create-quiz/create-quiz-page";

export const Route = createFileRoute("/quizzes/create")({
  component: CreateQuizPage,
});

import axios from "axios";
import { useEffect, useState } from "react";
import { useSuspenseQuery } from "@tanstack/react-query";
import { Link, createFileRoute, notFound } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import {
  Check,
  CircleSlash,
  ListChecks,
  RefreshCcw,
  Trophy,
} from "lucide-react";

import {
  getQuizQuestionsQueryOptions,
  getQuizQueryOptions,
} from "@/api/quiz/quizQueryOptions";
import { ComponentLoader } from "@/components/feedback/component-loader";
import type { MultipleChoiceQuestionObject } from "@/api/quiz/types";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/components/ui/empty";
import { Separator } from "@/components/ui/separator";
import type { Context } from "@/routes/__root";

export const Route = createFileRoute("/quizzes/$quizId/take")({
  loader,
  pendingMs: 100,
  pendingComponent: () => (
    <div className="flex flex-1 items-center justify-center p-4">
      <ComponentLoader />
    </div>
  ),
  component: QuizTakePage,
});

async function loader({
  context,
  params,
}: {
  context: Context;
  params: { quizId: string };
}) {
  try {
    await context.queryClient.ensureQueryData(
      getQuizQuestionsQueryOptions(params.quizId)
    );
  } catch (error) {
    if (
      axios.isAxiosError(error) &&
      (error.response?.status === 403 || error.response?.status === 404)
    ) {
      throw notFound();
    }

    throw error;
  }
}

function QuizTakePage() {
  const { quizId } = Route.useParams();
  const { t } = useTranslation();
  const { data: quiz } = useSuspenseQuery(getQuizQueryOptions(quizId));
  const { data: questionData } = useSuspenseQuery(
    getQuizQuestionsQueryOptions(quizId)
  );
  const questions = questionData.questions as MultipleChoiceQuestionObject[];
  const [selectedAnswers, setSelectedAnswers] = useState<Array<number | null>>(
    () => questions.map(() => null)
  );
  const [hasSubmitted, setHasSubmitted] = useState(false);

  useEffect(() => {
    setSelectedAnswers(questions.map(() => null));
    setHasSubmitted(false);
  }, [quizId, questions]);

  if (!questions.length) {
    return (
      <main className="mx-auto flex w-full max-w-4xl flex-1 flex-col gap-6 p-4 md:p-6">
        <Card>
          <CardContent className="pt-4">
            <Empty className="border-border/80 bg-muted/20 border">
              <EmptyHeader>
                <EmptyMedia variant="icon">
                  <ListChecks className="size-4" />
                </EmptyMedia>
                <EmptyTitle>{t("quizTakePage.empty.title")}</EmptyTitle>
                <EmptyDescription>
                  {t("quizTakePage.empty.description")}
                </EmptyDescription>
              </EmptyHeader>
            </Empty>
          </CardContent>
        </Card>
      </main>
    );
  }

  const totalQuestions = questions.length;
  const answeredQuestions = selectedAnswers.filter(
    (answer) => answer !== null
  ).length;
  const totalPoints = questions.reduce(
    (sum, question) => sum + question.points,
    0
  );
  const correctAnswers = questions.reduce((sum, question, questionIndex) => {
    const selectedAnswer = selectedAnswers[questionIndex];

    if (selectedAnswer === null) {
      return sum;
    }

    const options = getSortedOptions(question);

    return options[selectedAnswer]?.isCorrect ? sum + 1 : sum;
  }, 0);
  const earnedPoints = questions.reduce((sum, question, questionIndex) => {
    const selectedAnswer = selectedAnswers[questionIndex];

    if (selectedAnswer === null) {
      return sum;
    }

    const options = getSortedOptions(question);

    return options[selectedAnswer]?.isCorrect ? sum + question.points : sum;
  }, 0);
  const scorePercentage = totalPoints
    ? Math.round((earnedPoints / totalPoints) * 100)
    : 0;

  function handleAnswerSelect(questionIndex: number, optionIndex: number) {
    if (hasSubmitted) {
      return;
    }

    setSelectedAnswers((currentAnswers) => {
      const nextAnswers = [...currentAnswers];
      nextAnswers[questionIndex] = optionIndex;
      return nextAnswers;
    });
  }

  function handleSubmit() {
    setHasSubmitted(true);
  }

  function handleRetake() {
    setSelectedAnswers(questions.map(() => null));
    setHasSubmitted(false);
  }

  return (
    <main className="mx-auto flex w-full max-w-5xl flex-1 flex-col gap-6 p-4 md:p-6">
      <header className="border-border/70 from-background via-muted/40 to-background rounded-2xl border bg-linear-to-br p-6 shadow-sm">
        <div className="flex flex-col gap-4 lg:flex-row lg:items-end lg:justify-between">
          <div className="space-y-2">
            <p className="text-primary text-sm font-medium tracking-[0.2em] uppercase">
              {hasSubmitted
                ? t("quizTakePage.results.eyebrow")
                : t("quizTakePage.eyebrow")}
            </p>
            <h1 className="text-3xl font-semibold tracking-tight md:text-4xl">
              {quiz.title}
            </h1>
            <p className="text-muted-foreground max-w-3xl text-sm leading-6 md:text-base">
              {hasSubmitted
                ? t("quizTakePage.results.description")
                : t("quizTakePage.description")}
            </p>
          </div>
          <div className="flex flex-wrap gap-3">
            <Button
              type="button"
              variant="outline"
              nativeButton={false}
              render={
                <Link
                  to="/quizzes/$quizId"
                  params={{
                    quizId,
                  }}
                ></Link>
              }
            >
              {t("quizTakePage.actions.back")}
            </Button>
            {hasSubmitted ? (
              <Button type="button" onClick={handleRetake}>
                <RefreshCcw />
                {t("quizTakePage.actions.retake")}
              </Button>
            ) : (
              <Button type="button" onClick={handleSubmit}>
                <Trophy />
                {t("quizTakePage.actions.submit")}
              </Button>
            )}
          </div>
        </div>
      </header>

      <Card>
        <CardHeader>
          <CardTitle>
            {hasSubmitted
              ? t("quizTakePage.results.title")
              : t("quizTakePage.progress.title")}
          </CardTitle>
          <CardDescription>
            {hasSubmitted
              ? t("quizTakePage.results.summary")
              : t("quizTakePage.progress.description")}
          </CardDescription>
        </CardHeader>
        <CardContent className="grid gap-3 sm:grid-cols-2 xl:grid-cols-4">
          <StatCard
            label={t("quizTakePage.progress.answered")}
            value={`${answeredQuestions}/${totalQuestions}`}
          />
          <StatCard
            label={t("quizTakePage.results.correctAnswers")}
            value={`${correctAnswers}/${totalQuestions}`}
          />
          <StatCard
            label={t("quizTakePage.results.score")}
            value={`${earnedPoints}/${totalPoints}`}
          />
          <StatCard
            label={t("quizTakePage.results.percentage")}
            value={`${scorePercentage}%`}
          />
        </CardContent>
      </Card>

      <div className="space-y-4">
        {questions.map((question, questionIndex) => {
          const selectedAnswer = selectedAnswers[questionIndex];
          const options = getSortedOptions(question);

          return (
            <Card key={`${question.title}-${questionIndex}`}>
              <CardHeader>
                <CardTitle>
                  {t("quizTakePage.questionTitle", {
                    index: questionIndex + 1,
                  })}
                </CardTitle>
                <CardDescription className="space-y-1">
                  <span className="text-foreground block text-sm font-medium">
                    {question.title}
                  </span>
                  <span className="block">
                    {t("quizTakePage.questionPoints", {
                      points: question.points,
                    })}
                  </span>
                </CardDescription>
              </CardHeader>
              <CardContent className="space-y-3">
                {options.map((option, optionIndex) => {
                  const isSelected = selectedAnswer === optionIndex;
                  const isCorrect = option.isCorrect;
                  const isIncorrectSelection =
                    hasSubmitted && isSelected && !isCorrect;
                  const shouldHighlightCorrect = hasSubmitted && isCorrect;

                  return (
                    <Button
                      key={`${option.displayOrder}-${option.text}`}
                      type="button"
                      variant={isSelected ? "default" : "outline"}
                      className={getOptionClassName({
                        isIncorrectSelection,
                        isSelected,
                        shouldHighlightCorrect,
                      })}
                      onClick={() =>
                        handleAnswerSelect(questionIndex, optionIndex)
                      }
                    >
                      <span className="flex min-w-0 flex-1 flex-col items-start text-left">
                        <span className="whitespace-normal">{option.text}</span>
                        {hasSubmitted && isSelected && !isCorrect ? (
                          <span className="text-destructive mt-1 text-xs font-medium">
                            {t("quizTakePage.results.yourAnswer")}
                          </span>
                        ) : null}
                        {hasSubmitted && shouldHighlightCorrect ? (
                          <span className="mt-1 text-xs font-medium text-emerald-700 dark:text-emerald-400">
                            {isSelected
                              ? t("quizTakePage.results.correctAnswerSelected")
                              : t("quizTakePage.results.correctAnswer")}
                          </span>
                        ) : null}
                      </span>
                      {hasSubmitted ? (
                        shouldHighlightCorrect ? (
                          <Check className="size-4 shrink-0" />
                        ) : isIncorrectSelection ? (
                          <CircleSlash className="size-4 shrink-0" />
                        ) : null
                      ) : null}
                    </Button>
                  );
                })}

                {hasSubmitted && selectedAnswer === null ? (
                  <p className="text-muted-foreground text-sm leading-6">
                    {t("quizTakePage.results.unanswered")}
                  </p>
                ) : null}

                {questionIndex < questions.length - 1 ? <Separator /> : null}
              </CardContent>
            </Card>
          );
        })}
      </div>
    </main>
  );
}

function StatCard({ label, value }: { label: string; value: string }) {
  return (
    <div className="border-border/70 bg-muted/20 space-y-2 rounded-xl border px-4 py-3">
      <p className="text-muted-foreground text-xs font-medium tracking-wide uppercase">
        {label}
      </p>
      <p className="text-lg font-semibold">{value}</p>
    </div>
  );
}

function getSortedOptions(question: MultipleChoiceQuestionObject) {
  return [...question.options].sort(
    (left, right) => left.displayOrder - right.displayOrder
  );
}

function getOptionClassName({
  isIncorrectSelection,
  isSelected,
  shouldHighlightCorrect,
}: {
  isIncorrectSelection: boolean;
  isSelected: boolean;
  shouldHighlightCorrect: boolean;
}) {
  if (shouldHighlightCorrect) {
    return "h-auto w-full justify-start gap-3 border-emerald-500/60 bg-emerald-50 px-4 py-3 text-foreground hover:bg-emerald-100 dark:bg-emerald-950/30 dark:hover:bg-emerald-950/40";
  }

  if (isIncorrectSelection) {
    return "h-auto w-full justify-start gap-3 border-destructive/60 bg-destructive/10 px-4 py-3 text-foreground hover:bg-destructive/15";
  }

  if (isSelected) {
    return "h-auto w-full justify-start gap-3 px-4 py-3 text-left whitespace-normal";
  }

  return "h-auto w-full justify-start gap-3 px-4 py-3 text-left whitespace-normal";
}

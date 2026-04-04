import { useSuspenseQuery } from "@tanstack/react-query";
import { Link, createFileRoute } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import {
  BookOpenText,
  CircleUserRound,
  Globe,
  Languages,
  Play,
  Tags,
} from "lucide-react";

import { getQuizQueryOptions } from "@/api/quiz/quizQueryOptions";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";

export const Route = createFileRoute("/quizzes/$quizId/")({
  component: QuizDetailPage,
});

function QuizDetailPage() {
  const { quizId } = Route.useParams();
  const { t } = useTranslation();
  const { data: quiz } = useSuspenseQuery(getQuizQueryOptions(quizId));

  return (
    <main className="mx-auto flex w-full max-w-6xl flex-1 flex-col gap-6 p-4 md:p-6">
      <header className="border-border/70 from-background via-muted/40 to-background rounded-2xl border bg-linear-to-br p-6 shadow-sm">
        <div className="flex flex-col gap-6 lg:flex-row lg:items-start lg:justify-between">
          <div className="max-w-3xl space-y-3">
            <p className="text-primary text-sm font-medium tracking-[0.2em] uppercase">
              {t("quizDetailsPage.eyebrow")}
            </p>
            <div className="space-y-2">
              <h1 className="text-3xl font-semibold tracking-tight md:text-4xl">
                {quiz.title}
              </h1>
              <p className="text-muted-foreground text-sm leading-6 md:text-base">
                {quiz.description || t("quizDetailsPage.noDescription")}
              </p>
            </div>
          </div>
          <div className="flex w-full max-w-sm flex-col gap-3 lg:items-end">
            <Button
              size="xl"
              className="w-full lg:w-auto"
              nativeButton={false}
              render={
                <Link
                  to="/quizzes/$quizId/take"
                  params={{
                    quizId,
                  }}
                ></Link>
              }
            >
              <Play />
              {t("quizDetailsPage.actions.start")}
            </Button>
            <p className="text-muted-foreground text-sm leading-6 lg:text-right">
              {t("quizDetailsPage.actions.helper")}
            </p>
          </div>
        </div>
      </header>

      <div className="grid gap-6 xl:grid-cols-[minmax(0,1.6fr)_minmax(320px,0.9fr)]">
        <Card>
          <CardHeader>
            <CardTitle>{t("quizDetailsPage.overview.title")}</CardTitle>
            <CardDescription>
              {t("quizDetailsPage.overview.description")}
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-5">
            <div className="grid gap-3 sm:grid-cols-2">
              <QuizMeta
                icon={CircleUserRound}
                label={t("quizDetailsPage.overview.creator")}
                value={quiz.creatorName}
              />
              <QuizMeta
                icon={Languages}
                label={t("quizDetailsPage.overview.language")}
                value={quiz.languageCode.toUpperCase()}
              />
              <QuizMeta
                icon={BookOpenText}
                label={t("quizDetailsPage.overview.questions")}
                value={String(quiz.questionCount)}
              />
              <QuizMeta
                icon={Globe}
                label={t("quizDetailsPage.overview.attempts")}
                value={String(quiz.attemptCount)}
              />
            </div>

            <Separator />

            <section className="space-y-3">
              <div className="flex items-center gap-2">
                <Tags className="text-muted-foreground size-4" />
                <h2 className="text-sm font-medium">
                  {t("quizDetailsPage.overview.topics")}
                </h2>
              </div>
              {quiz.topics.length ? (
                <div className="flex flex-wrap gap-2">
                  {quiz.topics.map((topic) => (
                    <span
                      key={topic}
                      className="border-border bg-muted text-muted-foreground rounded-full border px-3 py-1 text-xs font-medium"
                    >
                      {topic}
                    </span>
                  ))}
                </div>
              ) : (
                <p className="text-muted-foreground text-sm leading-6">
                  {t("quizDetailsPage.noTopics")}
                </p>
              )}
            </section>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>{t("quizDetailsPage.ready.title")}</CardTitle>
            <CardDescription>
              {t("quizDetailsPage.ready.description")}
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-4">
            <ReadyRow
              label={t("quizDetailsPage.ready.questionCount")}
              value={String(quiz.questionCount)}
            />
            <ReadyRow
              label={t("quizDetailsPage.ready.visibility")}
              value={t("quizDetailsPage.ready.public")}
            />
            <ReadyRow
              label={t("quizDetailsPage.ready.attemptCount")}
              value={String(quiz.attemptCount)}
            />
            <ReadyRow
              label={t("quizDetailsPage.ready.access")}
              value={t("quizDetailsPage.ready.guestAccess")}
            />
          </CardContent>
        </Card>
      </div>
    </main>
  );
}

function QuizMeta({
  icon: Icon,
  label,
  value,
}: {
  icon: typeof Globe;
  label: string;
  value: string;
}) {
  return (
    <div className="border-border/70 bg-muted/20 flex items-center gap-3 rounded-xl border px-4 py-3">
      <div className="bg-background flex size-9 items-center justify-center rounded-full border">
        <Icon className="size-4" />
      </div>
      <div className="min-w-0 space-y-1">
        <p className="text-muted-foreground text-xs font-medium tracking-wide uppercase">
          {label}
        </p>
        <p className="truncate text-sm font-medium">{value}</p>
      </div>
    </div>
  );
}

function ReadyRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="border-border/70 bg-muted/20 flex items-center justify-between rounded-lg border px-4 py-3">
      <span className="text-muted-foreground text-sm">{label}</span>
      <span className="text-sm font-medium">{value}</span>
    </div>
  );
}

import { Link } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import {
  BookOpenText,
  Globe,
  LockKeyhole,
  ListChecks,
  Sparkles,
} from "lucide-react";

import AlertError from "@/components/feedback/alert-error";
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
import { Spinner } from "@/components/ui/spinner";
import type { QuizResponse } from "@/api/quiz/types";

function MyQuizzesSection({
  quizzes,
  isPending,
  isError,
}: {
  quizzes?: QuizResponse[];
  isPending: boolean;
  isError: boolean;
}) {
  const { t } = useTranslation();

  return (
    <Card>
      <CardHeader>
        <CardTitle>{t("profilePage.quizzes.title")}</CardTitle>
        <CardDescription>
          {t("profilePage.quizzes.description")}
        </CardDescription>
      </CardHeader>
      <CardContent>
        {isPending ? (
          <div className="flex min-h-32 items-center justify-center">
            <Spinner className="size-6" />
          </div>
        ) : isError ? (
          <AlertError
            error={{
              title: t("profilePage.quizzes.error.title"),
              description: t("profilePage.quizzes.error.description"),
            }}
          />
        ) : !quizzes?.length ? (
          <Empty className="border-border/80 bg-muted/20 border border-dashed">
            <EmptyHeader>
              <EmptyMedia variant="icon">
                <BookOpenText className="size-4" />
              </EmptyMedia>
              <EmptyTitle>{t("profilePage.quizzes.empty.title")}</EmptyTitle>
              <EmptyDescription>
                {t("profilePage.quizzes.empty.description")}
              </EmptyDescription>
            </EmptyHeader>
          </Empty>
        ) : (
          <div className="space-y-4">
            {quizzes.map((quiz, index) => (
              <div key={quiz.id} className="space-y-4">
                <article className="flex flex-col gap-3 lg:flex-row lg:items-start lg:justify-between">
                  <div className="space-y-2">
                    <div className="flex flex-wrap items-center gap-2">
                      <h3 className="text-base font-medium">{quiz.title}</h3>
                      <span className="border-border bg-muted text-muted-foreground rounded-full border px-2 py-0.5 text-xs font-medium">
                        {quiz.isPublic
                          ? t("profilePage.quizzes.public")
                          : t("profilePage.quizzes.private")}
                      </span>
                    </div>
                    <p className="text-muted-foreground max-w-3xl text-sm leading-6">
                      {quiz.description ||
                        t("profilePage.quizzes.noDescription")}
                    </p>
                  </div>
                  <div className="flex flex-col gap-3 lg:items-end">
                    <div className="text-muted-foreground grid gap-2 text-sm sm:grid-cols-2 lg:min-w-72 lg:justify-items-end">
                      <QuizMeta
                        icon={quiz.isPublic ? Globe : LockKeyhole}
                        label={t("profilePage.quizzes.visibility")}
                        value={
                          quiz.isPublic
                            ? t("profilePage.quizzes.public")
                            : t("profilePage.quizzes.private")
                        }
                      />
                      <QuizMeta
                        icon={Sparkles}
                        label={t("profilePage.quizzes.language")}
                        value={quiz.languageCode.toUpperCase()}
                      />
                      <QuizMeta
                        icon={ListChecks}
                        label={t("profilePage.quizzes.questions")}
                        value={String(quiz.questionCount)}
                      />
                      <QuizMeta
                        icon={BookOpenText}
                        label={t("profilePage.quizzes.attempts")}
                        value={String(quiz.attemptCount)}
                      />
                    </div>
                    <Button
                      variant="outline"
                      nativeButton={false}
                      render={
                        <Link
                          to="/quizzes/$quizId"
                          params={{
                            quizId: quiz.id,
                          }}
                        ></Link>
                      }
                    >
                      {t("profilePage.quizzes.viewQuiz")}
                    </Button>
                  </div>
                </article>
                {index < quizzes.length - 1 && <Separator />}
              </div>
            ))}
          </div>
        )}
      </CardContent>
    </Card>
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
    <div className="border-border/70 bg-muted/20 flex items-center gap-2 rounded-lg border px-3 py-2">
      <Icon className="size-3.5" />
      <span className="text-xs font-medium tracking-wide uppercase">
        {label}
      </span>
      <span className="text-foreground ml-auto font-medium">{value}</span>
    </div>
  );
}

export default MyQuizzesSection;

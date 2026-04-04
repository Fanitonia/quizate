import { Link } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import {
  BarChart3,
  BookOpenText,
  CircleUserRound,
  Languages,
  ListChecks,
  Tags,
} from "lucide-react";

import type { GetQuizzesResponse } from "@/api/quiz/types";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
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
import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";

function HomeQuizListSection({
  quizzesResponse,
  isFetching,
  onPageChange,
}: {
  quizzesResponse: GetQuizzesResponse;
  isFetching: boolean;
  onPageChange: (page: number) => void;
}) {
  const { t, i18n } = useTranslation();
  const { quizzes, pagination } = quizzesResponse;
  const paginationItems = getPaginationItems(
    pagination.currentPage,
    pagination.totalPages
  );

  return (
    <Card id="quiz-list">
      <CardHeader className="gap-3">
        <div className="flex flex-col gap-3 lg:flex-row lg:items-end lg:justify-between">
          <div className="space-y-1">
            <CardTitle>{t("homePage.list.title")}</CardTitle>
            <CardDescription>{t("homePage.list.description")}</CardDescription>
          </div>
          <div className="flex flex-wrap gap-2 text-xs sm:text-sm">
            <span className="border-border/70 bg-muted/40 text-muted-foreground rounded-full border px-3 py-1">
              {t("homePage.list.pageSummary", {
                currentPage: pagination.currentPage,
                totalPages: pagination.totalPages,
              })}
            </span>
            <span className="border-border/70 bg-muted/40 text-muted-foreground rounded-full border px-3 py-1">
              {t("homePage.list.totalResults", {
                count: pagination.totalCount.toLocaleString(i18n.language),
              })}
            </span>
          </div>
        </div>
      </CardHeader>

      <CardContent className="space-y-6">
        {!quizzes.length ? (
          <Empty className="border-border/80 bg-muted/20 border border-dashed">
            <EmptyHeader>
              <EmptyMedia variant="icon">
                <BookOpenText className="size-4" />
              </EmptyMedia>
              <EmptyTitle>{t("homePage.list.empty.title")}</EmptyTitle>
              <EmptyDescription>
                {t("homePage.list.empty.description")}
              </EmptyDescription>
            </EmptyHeader>
          </Empty>
        ) : (
          <>
            <div className="grid gap-4 pb-4 xl:grid-cols-2">
              {quizzes.map((quiz) => (
                <Card
                  key={quiz.id}
                  className="border-border/80 bg-background h-full border shadow-sm transition-shadow hover:shadow-md"
                >
                  <CardHeader className="space-y-4">
                    <div className="flex items-start justify-between gap-3">
                      <div className="min-w-0 space-y-2">
                        <CardTitle className="text-lg">{quiz.title}</CardTitle>
                        <CardDescription className="line-clamp-3 text-sm leading-6">
                          {quiz.description || t("homePage.list.noDescription")}
                        </CardDescription>
                      </div>
                      <span className="border-border bg-muted text-muted-foreground shrink-0 rounded-full border px-3 py-1 text-xs font-medium uppercase">
                        {quiz.languageCode}
                      </span>
                    </div>

                    <div className="flex min-h-8 flex-wrap gap-2">
                      {quiz.topics.length ? (
                        quiz.topics.map((topic) => (
                          <span
                            key={topic}
                            className="border-border/70 bg-muted/30 text-muted-foreground rounded-full border px-3 py-1 text-xs font-medium"
                          >
                            {topic}
                          </span>
                        ))
                      ) : (
                        <span className="text-muted-foreground text-sm">
                          {t("homePage.list.noTopics")}
                        </span>
                      )}
                    </div>
                  </CardHeader>

                  <CardContent className="grid gap-3 sm:grid-cols-2">
                    <QuizMeta
                      icon={CircleUserRound}
                      label={t("homePage.list.creator")}
                      value={
                        quiz.creatorName ?? t("homePage.list.unknownCreator")
                      }
                    />
                    <QuizMeta
                      icon={Languages}
                      label={t("homePage.list.language")}
                      value={quiz.languageCode.toUpperCase()}
                    />
                    <QuizMeta
                      icon={ListChecks}
                      label={t("homePage.list.questions")}
                      value={String(quiz.questionCount)}
                    />
                    <QuizMeta
                      icon={BarChart3}
                      label={t("homePage.list.attempts")}
                      value={String(quiz.attemptCount)}
                    />
                  </CardContent>

                  <CardFooter className="mt-auto justify-between gap-3">
                    <div className="text-muted-foreground flex items-center gap-2 text-sm">
                      <Tags className="size-4" />
                      <span>{t("homePage.list.topics")}</span>
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
                      {t("homePage.list.viewQuiz")}
                    </Button>
                  </CardFooter>
                </Card>
              ))}
            </div>

            {pagination.totalPages > 1 ? (
              <>
                <Separator />
                <Pagination className="mb-4">
                  <PaginationContent>
                    <PaginationItem>
                      {pagination.hasPrevious ? (
                        <PaginationPrevious
                          aria-label={t("homePage.list.pagination.previous")}
                          disabled={isFetching}
                          onClick={() => {
                            onPageChange(pagination.currentPage - 1);
                          }}
                        >
                          {t("homePage.list.pagination.previous")}
                        </PaginationPrevious>
                      ) : (
                        <PaginationPrevious disabled>
                          {t("homePage.list.pagination.previous")}
                        </PaginationPrevious>
                      )}
                    </PaginationItem>

                    {paginationItems.map((item, index) => (
                      <PaginationItem key={`${item}-${index}`}>
                        {item === "ellipsis" ? (
                          <PaginationEllipsis />
                        ) : item === pagination.currentPage ? (
                          <PaginationLink
                            isActive
                            aria-label={t("homePage.list.pagination.goToPage", {
                              page: item,
                            })}
                          >
                            {item}
                          </PaginationLink>
                        ) : (
                          <PaginationLink
                            disabled={isFetching}
                            aria-label={t("homePage.list.pagination.goToPage", {
                              page: item,
                            })}
                            onClick={() => {
                              onPageChange(item);
                            }}
                          >
                            {item}
                          </PaginationLink>
                        )}
                      </PaginationItem>
                    ))}

                    <PaginationItem>
                      {pagination.hasNext ? (
                        <PaginationNext
                          aria-label={t("homePage.list.pagination.next")}
                          disabled={isFetching}
                          onClick={() => {
                            onPageChange(pagination.currentPage + 1);
                          }}
                        >
                          {t("homePage.list.pagination.next")}
                        </PaginationNext>
                      ) : (
                        <PaginationNext disabled>
                          {t("homePage.list.pagination.next")}
                        </PaginationNext>
                      )}
                    </PaginationItem>
                  </PaginationContent>
                </Pagination>
              </>
            ) : null}
          </>
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
  icon: typeof CircleUserRound;
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

function getPaginationItems(currentPage: number, totalPages: number) {
  if (totalPages <= 7) {
    return Array.from({ length: totalPages }, (_, index) => index + 1);
  }

  const pages = new Set<number>([
    1,
    totalPages,
    currentPage - 1,
    currentPage,
    currentPage + 1,
  ]);

  if (currentPage <= 3) {
    pages.add(2);
    pages.add(3);
    pages.add(4);
  }

  if (currentPage >= totalPages - 2) {
    pages.add(totalPages - 1);
    pages.add(totalPages - 2);
    pages.add(totalPages - 3);
  }

  const sortedPages = [...pages]
    .filter((page) => page >= 1 && page <= totalPages)
    .sort((left, right) => left - right);

  return sortedPages.reduce<Array<number | "ellipsis">>(
    (items, page, index) => {
      const previousPage = sortedPages[index - 1];

      if (previousPage && page - previousPage > 1) {
        items.push("ellipsis");
      }

      items.push(page);
      return items;
    },
    []
  );
}

export default HomeQuizListSection;

import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { Link } from "@tanstack/react-router";
import { useState } from "react";
import { useTranslation } from "react-i18next";

import { getQuizzesQueryOptions } from "@/api/quiz/quizQueryOptions";
import SomethingGoneWrong from "@/components/feedback/gone-wrong";
import { ComponentLoader } from "@/components/feedback/component-loader";
import { Button } from "@/components/ui/button";

import HomeQuizListSection from "./home-quiz-list-section";

function HomePage({
  defaultPage,
  defaultPageSize,
}: {
  defaultPage: number;
  defaultPageSize: number;
}) {
  const { t } = useTranslation();
  const [page, setPage] = useState(defaultPage);

  function handlePageChange(nextPage: number) {
    setPage(nextPage);
    window.scrollTo({
      top: 0,
      behavior: "auto",
    });
  }

  const {
    data: quizzesResponse,
    isPending,
    isError,
    isFetching,
  } = useQuery({
    ...getQuizzesQueryOptions(page, defaultPageSize),
    placeholderData: keepPreviousData,
  });

  if (isPending && !quizzesResponse) {
    return (
      <main className="mx-auto flex w-full max-w-6xl flex-1 items-center justify-center p-4 md:p-6">
        <ComponentLoader />
      </main>
    );
  }

  if (isError && !quizzesResponse) {
    return (
      <main className="mx-auto flex w-full max-w-6xl flex-1 p-4 md:p-6">
        <div className="w-full">
          <SomethingGoneWrong />
        </div>
      </main>
    );
  }

  if (!quizzesResponse) {
    return null;
  }

  const { pagination } = quizzesResponse;

  return (
    <main className="mx-auto flex w-full max-w-6xl flex-1 flex-col gap-6 p-4 md:p-6">
      <header className="border-border/70 from-primary/5 via-background to-muted/60 overflow-hidden rounded-3xl border bg-linear-to-br shadow-sm">
        <div className="flex flex-col gap-8 p-6 md:p-8">
          <div className="flex flex-col gap-6 lg:flex-row lg:items-end lg:justify-between">
            <div className="max-w-3xl space-y-4">
              <p className="text-primary text-sm font-medium tracking-[0.24em] uppercase">
                {t("homePage.hero.eyebrow")}
              </p>
              <div className="space-y-3">
                <h1 className="text-3xl font-semibold tracking-tight md:text-5xl">
                  {t("homePage.hero.title")}
                </h1>
                <p className="text-muted-foreground max-w-2xl text-sm leading-6 md:text-base">
                  {t("homePage.hero.description")}
                </p>
              </div>
            </div>
            <div className="flex flex-wrap gap-3">
              <Button
                size="xl"
                nativeButton={false}
                render={<a href="#quiz-list"></a>}
              >
                {t("homePage.hero.browseAction")}
              </Button>
              <Button
                size="xl"
                variant="outline"
                nativeButton={false}
                render={<Link to="/quizzes/create"></Link>}
              >
                {t("homePage.hero.createAction")}
              </Button>
            </div>
          </div>

          <div className="max-w-xs">
            <HeroStat
              label={t("homePage.hero.stats.totalQuizzes")}
              value={pagination.totalCount.toString()}
            />
          </div>
        </div>
      </header>

      <HomeQuizListSection
        quizzesResponse={quizzesResponse}
        isFetching={isFetching}
        onPageChange={handlePageChange}
      />
    </main>
  );
}

function HeroStat({ label, value }: { label: string; value: string }) {
  return (
    <div className="border-border/70 bg-background/70 rounded-2xl border px-4 py-4 backdrop-blur-sm">
      <p className="text-muted-foreground text-xs font-medium tracking-wide uppercase">
        {label}
      </p>
      <p className="mt-2 text-2xl font-semibold tracking-tight">{value}</p>
    </div>
  );
}

export default HomePage;

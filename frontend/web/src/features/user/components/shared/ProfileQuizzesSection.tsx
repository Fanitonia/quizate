import { useTranslation } from "react-i18next";

import { BookOpen } from "lucide-react";

import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@components/ui/empty";

function ProfileQuizzesSection({ username }: { username: string }) {
  const { t } = useTranslation();

  return (
    <section className="flex flex-col gap-2 ">
      <div className="pl-2">
        <h2 className="text-lg">
          {t("profilePage.quizzes.createdQuizzes", { username })}
        </h2>
      </div>

      <div className="border rounded-xl">
        <Empty>
          <EmptyHeader>
            <EmptyMedia variant="icon">
              <BookOpen />
            </EmptyMedia>
            <EmptyTitle>{t("profilePage.quizzes.emptyTitle")}</EmptyTitle>
            <EmptyDescription>
              {t("profilePage.quizzes.emptyDescription")}
            </EmptyDescription>
          </EmptyHeader>
        </Empty>
      </div>
    </section>
  );
}

export { ProfileQuizzesSection };

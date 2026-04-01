import { useTranslation } from "react-i18next";

import { useMyQuizzesQuery } from "@/api/user/myQuizzesQueryOptions";
import { useCurrentUserQuery } from "@/api/user/currentUserQueryOptions";
import AlertError from "@/components/feedback/alert-error";
import { ComponentLoader } from "@/components/feedback/component-loader";

import MyQuizzesSection from "./my-quizzes-section";
import ProfileActionSheets from "./profile-action-sheets";
import ProfileOverviewCard from "./profile-overview-card";

function ProfilePage() {
  const { t } = useTranslation();

  const {
    data: currentUser,
    isPending: isCurrentUserPending,
    isError: isCurrentUserError,
  } = useCurrentUserQuery();

  const {
    data: quizzes,
    isPending: isMyQuizzesPending,
    isError: isMyQuizzesError,
  } = useMyQuizzesQuery();

  if (isCurrentUserPending) {
    return (
      <div className="flex flex-1 items-center justify-center p-4">
        <ComponentLoader />
      </div>
    );
  }

  if (isCurrentUserError || !currentUser) {
    return (
      <main className="mx-auto flex w-full max-w-5xl flex-1 p-4 md:p-6">
        <div className="w-full">
          <AlertError
            error={{
              title: t("profilePage.error.title"),
              description: t("profilePage.error.description"),
            }}
          />
        </div>
      </main>
    );
  }

  return (
    <main className="mx-auto flex w-full max-w-6xl flex-1 flex-col gap-6 p-4 md:p-6">
      <header className="flex flex-col gap-4 lg:flex-row lg:items-end lg:justify-between">
        <div className="space-y-1">
          <h1 className="text-2xl font-semibold tracking-tight">
            {t("profilePage.title")}
          </h1>
          <p className="text-muted-foreground max-w-3xl text-sm leading-6">
            {t("profilePage.description")}
          </p>
        </div>
        <ProfileActionSheets currentUser={currentUser} />
      </header>
      <ProfileOverviewCard currentUser={currentUser} />
      <MyQuizzesSection
        quizzes={quizzes}
        isPending={isMyQuizzesPending}
        isError={isMyQuizzesError}
      />
    </main>
  );
}

export default ProfilePage;

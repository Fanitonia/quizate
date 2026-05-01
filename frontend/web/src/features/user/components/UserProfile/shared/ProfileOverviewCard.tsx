import { useTranslation } from "react-i18next";

import type { UserInfo } from "@type/api/users";

import { Lock, PencilLine, User } from "lucide-react";

import { Button } from "@components/ui/button";
import { Card, CardContent } from "@components/ui/card";
import { UserAvatar } from "@components/user/UserAvatar";

interface ProfileOverviewCardProps {
  userData: UserInfo;
  showActions?: boolean;
}

function formatJoinedDate(createdAt: string, language: string) {
  const createdDate = new Date(createdAt);

  if (Number.isNaN(createdDate.getTime())) {
    return createdAt;
  }

  return new Intl.DateTimeFormat(language, {
    month: "long",
    day: "numeric",
    year: "numeric",
  }).format(createdDate);
}

function ProfileOverviewCard({
  userData,
  showActions = false,
}: ProfileOverviewCardProps) {
  const { i18n, t } = useTranslation();
  const joinedDate = formatJoinedDate(userData.createdAt, i18n.language);

  return (
    <div className="flex flex-col justify-between gap-4 mt-4">
      {/* User Info Section */}
      <section className="flex items-start gap-4">
        <UserAvatar classNames="shrink-0 my-auto" size="xl" user={userData} />

        <div>
          <div className="space-y-1">
            <h1 className="text-2xl font-semibold tracking-tight">
              {userData.displayName}
            </h1>
            <p className="text-muted-foreground text-sm">
              @{userData.username}
            </p>
            <p className="text-muted-foreground text-sm">
              {t("profilePage.memberSince", { date: joinedDate })}
            </p>
          </div>
        </div>
      </section>
      {/* Actions */}
      {showActions && (
        <section className="flex flex-row gap-2 w-full flex-wrap">
          {/* TODO: implement actual actions */}
          <Button size="lg" type="button" variant="outline" className="flex-1">
            <PencilLine />
            {t("profilePage.actions.editProfile")}
          </Button>
          <Button size="lg" type="button" variant="outline" className="flex-1">
            <Lock />
            {t("profilePage.actions.changePassword")}
          </Button>
          <Button size="lg" type="button" variant="outline" className="flex-1">
            <User />
            {t("profilePage.actions.accountDetails")}
          </Button>
        </section>
      )}
      {/* Stats Section */}
      <Card>
        <CardContent className="flex flex-row w-full">
          <div className="flex flex-col flex-1 gap-1 text-center">
            <p className="">{t("profilePage.stats.created")}</p>
            {/* TODO: Replace with actual count */}
            <p className="text-base font-bold">0</p>
          </div>
          <div className="flex flex-col flex-1 gap-1 text-center ">
            <p className="">{t("profilePage.stats.solved")}</p>
            {/* TODO: Replace with actual count */}
            <p className="text-base  font-bold">0</p>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}

export { ProfileOverviewCard };

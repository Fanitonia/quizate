import { useTranslation } from "react-i18next";
import {
  BadgeCheck,
  CalendarDays,
  Mail,
  Shield,
  UserRound,
} from "lucide-react";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import type { DetailedUserInfoResponse } from "@/api/user/types";

function ProfileOverviewCard({
  currentUser,
}: {
  currentUser: DetailedUserInfoResponse;
}) {
  const { t, i18n } = useTranslation();

  return (
    <Card>
      <CardHeader>
        <CardTitle>{t("profilePage.overview.title")}</CardTitle>
        <CardDescription>
          {t("profilePage.overview.description")}
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-5">
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-4">
            <Avatar size="lg" className="size-16 cursor-default">
              <AvatarImage src={currentUser.profilePictureUrl ?? undefined} />
              <AvatarFallback className="text-lg font-semibold">
                {getInitials(currentUser.username)}
              </AvatarFallback>
            </Avatar>
            <div className="space-y-1">
              <div className="flex flex-wrap items-center gap-2">
                <h2 className="text-xl font-semibold tracking-tight">
                  {currentUser.username}
                </h2>
                <span className="border-border bg-muted text-muted-foreground rounded-full border px-2 py-0.5 text-xs font-medium">
                  {currentUser.role}
                </span>
              </div>
              <p className="text-muted-foreground text-sm">
                {currentUser.email || t("profilePage.overview.noEmail")}
              </p>
            </div>
          </div>
          <span
            className="border-border bg-background inline-flex w-fit items-center gap-2 rounded-full border px-3 py-1 text-xs font-medium"
            aria-live="polite"
          >
            <BadgeCheck className="size-3.5" />
            {currentUser.isEmailVerified
              ? t("profilePage.overview.verified")
              : t("profilePage.overview.notVerified")}
          </span>
        </div>
        <Separator />
        <dl className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
          <OverviewStat
            icon={UserRound}
            label={t("username")}
            value={currentUser.username}
          />
          <OverviewStat
            icon={Mail}
            label={t("email")}
            value={currentUser.email || t("profilePage.overview.noEmail")}
          />
          <OverviewStat
            icon={Shield}
            label={t("profilePage.overview.emailStatus")}
            value={
              currentUser.isEmailVerified
                ? t("profilePage.overview.verified")
                : t("profilePage.overview.notVerified")
            }
          />
          <OverviewStat
            icon={CalendarDays}
            label={t("profilePage.overview.memberSince")}
            value={new Intl.DateTimeFormat(i18n.language, {
              dateStyle: "medium",
            }).format(new Date(currentUser.createdAt))}
          />
        </dl>
      </CardContent>
    </Card>
  );
}

function OverviewStat({
  icon: Icon,
  label,
  value,
}: {
  icon: typeof UserRound;
  label: string;
  value: string;
}) {
  return (
    <div className="border-border/70 bg-muted/30 rounded-xl border p-4">
      <dt className="text-muted-foreground mb-2 flex items-center gap-2 text-xs font-medium tracking-wide uppercase">
        <Icon className="size-3.5" />
        {label}
      </dt>
      <dd className="text-sm font-medium wrap-break-word">{value}</dd>
    </div>
  );
}

function getInitials(username: string) {
  return username
    .split(/[\s_]+/)
    .filter(Boolean)
    .slice(0, 2)
    .map((part) => part[0]?.toUpperCase())
    .join("");
}

export default ProfileOverviewCard;

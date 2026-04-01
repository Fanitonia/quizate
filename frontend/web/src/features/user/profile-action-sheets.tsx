import { KeyRound, Settings2 } from "lucide-react";
import { useTranslation } from "react-i18next";

import { Button } from "@/components/ui/button";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import type { DetailedUserResponse } from "@/api/user/types";

import ChangePasswordForm from "./change-password-form";
import ProfileSettingsForm from "./profile-settings-form";

function ProfileActionSheets({
  currentUser,
}: {
  currentUser: DetailedUserResponse;
}) {
  const { t } = useTranslation();

  return (
    <div className="flex flex-wrap items-center gap-2">
      <Sheet>
        <SheetTrigger render={<Button variant="outline" size="sm" />}>
          <Settings2 />
          {t("profilePage.actions.openSettings")}
        </SheetTrigger>
        <SheetContent
          side="right"
          className="gap-0 data-[side=right]:w-full data-[side=right]:sm:max-w-xl"
        >
          <SheetHeader className="border-b pb-4">
            <SheetTitle>{t("profilePage.settings.title")}</SheetTitle>
            <SheetDescription>
              {t("profilePage.settings.description")}
            </SheetDescription>
          </SheetHeader>
          <div className="min-h-0 flex-1 overflow-y-auto pt-4">
            <ProfileSettingsForm currentUser={currentUser} embedded />
          </div>
        </SheetContent>
      </Sheet>

      <Sheet>
        <SheetTrigger render={<Button variant="outline" size="sm" />}>
          <KeyRound />
          {t("profilePage.actions.openPassword")}
        </SheetTrigger>
        <SheetContent
          side="right"
          className="gap-0 data-[side=right]:w-full data-[side=right]:sm:max-w-xl"
        >
          <SheetHeader className="border-b pb-4">
            <SheetTitle>{t("profilePage.password.title")}</SheetTitle>
            <SheetDescription>
              {t("profilePage.password.description")}
            </SheetDescription>
          </SheetHeader>
          <div className="min-h-0 flex-1 overflow-y-auto pt-4">
            <ChangePasswordForm embedded />
          </div>
        </SheetContent>
      </Sheet>
    </div>
  );
}

export default ProfileActionSheets;

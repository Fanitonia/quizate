import { useTranslation } from "react-i18next";

import { CircleAlert } from "lucide-react";

import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@components/ui/empty";

function SomethingGoneWrong() {
  const { t } = useTranslation();

  return (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="default">
          <CircleAlert size="48" className="text-destructive" />
        </EmptyMedia>
        <EmptyTitle className="text-2xl">{t("goneWrong.title")}</EmptyTitle>
        <EmptyDescription className="text-md">
          {t("goneWrong.description")}
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  );
}

export { SomethingGoneWrong };

// COMPONENTS & ICONS
import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/components/ui/empty";
import { CircleAlert } from "lucide-react";

// EXTERNAL
import { useTranslation } from "react-i18next";

export default function SomethingGoneWrong() {
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

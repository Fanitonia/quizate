// COMPONENTS & ICONS
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/components/ui/empty";
import { OctagonAlert } from "lucide-react";

// EXTERNAL
import { useTranslation } from "react-i18next";

function NotFound() {
  const { t } = useTranslation();

  return (
    <Empty className="p-8 text-center">
      <EmptyContent>
        <EmptyMedia>
          <OctagonAlert size={40} className="text-destructive" />
        </EmptyMedia>
        <EmptyHeader>
          <EmptyTitle className="text-xl">{t("notFound.title")}</EmptyTitle>
        </EmptyHeader>
        <EmptyDescription className="text-base">
          {t("notFound.description")}
        </EmptyDescription>
      </EmptyContent>
    </Empty>
  );
}

export default NotFound;

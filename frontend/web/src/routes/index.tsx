// EXTERNAL LIBRARIES
import { createFileRoute } from "@tanstack/react-router";

// COMPONENTS
import { Construction } from "lucide-react";
import { useTranslation } from "react-i18next";

export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  const { t } = useTranslation();

  return (
    <div className="flex flex-1 flex-col items-center justify-center gap-2 p-4">
      <Construction size="48" />
      <h3 className="text-foreground text-2xl">{t("workInProgress")}</h3>
    </div>
  );
}

/* eslint-disable react-refresh/only-export-components */
import { createFileRoute } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";

import { Construction } from "lucide-react";

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

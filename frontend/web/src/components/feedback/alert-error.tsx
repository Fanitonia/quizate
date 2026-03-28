// COMPONENTS & ICONS
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { CircleAlert } from "lucide-react";

// EXTERNAL
import { useTranslation } from "react-i18next";

interface AlertErrorProps {
  className?: string;
  error?: {
    title: string;
    description: string;
  };
}

function AlertError({ className, error }: AlertErrorProps) {
  const { t } = useTranslation();

  return (
    <Alert variant="destructive" className={className}>
      <CircleAlert />
      <AlertTitle>{error?.title || t("alertError.title")}</AlertTitle>
      <AlertDescription>
        {error?.description || t("alertError.description")}
      </AlertDescription>
    </Alert>
  );
}

export default AlertError;

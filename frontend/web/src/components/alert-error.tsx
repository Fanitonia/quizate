import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { CircleAlert } from "lucide-react";

interface AlertErrorProps {
  className?: string;
  error?: {
    title: string;
    description: string;
  };
}

function AlertError({ className, error }: AlertErrorProps) {
  return (
    <Alert variant="destructive" className={className}>
      <CircleAlert />
      <AlertTitle>{error?.title || "Error"}</AlertTitle>
      <AlertDescription>
        {error?.description || "An error occurred. Please try again."}
      </AlertDescription>
    </Alert>
  );
}

export default AlertError;

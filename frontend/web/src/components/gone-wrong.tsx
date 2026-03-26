import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/components/ui/empty";
import { CircleAlert } from "lucide-react";

export default function SomethingGoneWrong() {
  return (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="default">
          <CircleAlert size="48" className="text-destructive" />
        </EmptyMedia>
        <EmptyTitle className="text-2xl">Something Gone Wrong</EmptyTitle>
        <EmptyDescription className="text-md">
          An unexpected error occurred. Please try again later.
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  );
}

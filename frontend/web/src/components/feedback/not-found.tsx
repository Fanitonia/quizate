import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@/components/ui/empty";
import { OctagonAlert } from "lucide-react";

function NotFound() {
  return (
    <Empty className="p-8">
      <EmptyContent>
        <EmptyMedia>
          <OctagonAlert size={40} className="text-destructive" />
        </EmptyMedia>
        <EmptyHeader>
          <EmptyTitle className="text-xl">404 - Page Not Found</EmptyTitle>
        </EmptyHeader>
        <EmptyDescription className="text-base">
          The page you are looking for does not exist.
        </EmptyDescription>
      </EmptyContent>
    </Empty>
  );
}

export default NotFound;

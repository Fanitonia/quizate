import { OctagonAlert } from "lucide-react";

import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "@components/ui/empty";

function NotFound({
  title,
  description,
  icon,
}: {
  title: string;
  description: string;
  icon?: React.ReactNode;
}) {
  return (
    <Empty className="p-8 text-center">
      <EmptyContent>
        <EmptyMedia>
          {icon ? (
            icon
          ) : (
            <OctagonAlert size={40} className="text-destructive" />
          )}
        </EmptyMedia>
        <EmptyHeader>
          <EmptyTitle className="text-xl">{title}</EmptyTitle>
        </EmptyHeader>
        <EmptyDescription className="text-base">{description}</EmptyDescription>
      </EmptyContent>
    </Empty>
  );
}

export { NotFound };

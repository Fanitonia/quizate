import { createFileRoute } from "@tanstack/react-router";

import { getQuizzesQueryOptions } from "@/api/quiz/quizQueryOptions";
import SomethingGoneWrong from "@/components/feedback/gone-wrong";
import { ComponentLoader } from "@/components/feedback/component-loader";
import HomePage from "@/features/home/home-page";
import type { Context } from "@/routes/__root";

const DEFAULT_HOME_PAGE = 1;
const DEFAULT_HOME_PAGE_SIZE = 12;

export const Route = createFileRoute("/")({
  loader,
  pendingMs: 100,
  pendingComponent: () => (
    <div className="flex flex-1 items-center justify-center p-4">
      <ComponentLoader />
    </div>
  ),
  errorComponent: () => <SomethingGoneWrong />,
  component: Index,
});

function Index() {
  return (
    <HomePage
      defaultPage={DEFAULT_HOME_PAGE}
      defaultPageSize={DEFAULT_HOME_PAGE_SIZE}
    />
  );
}

async function loader({ context }: { context: Context }) {
  await context.queryClient.ensureQueryData(
    getQuizzesQueryOptions(DEFAULT_HOME_PAGE, DEFAULT_HOME_PAGE_SIZE)
  );
}

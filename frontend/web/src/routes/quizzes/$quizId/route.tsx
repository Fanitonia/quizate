import axios from "axios";
import { Outlet, createFileRoute, notFound } from "@tanstack/react-router";

import { getQuizQueryOptions } from "@/api/quiz/quizQueryOptions";
import { ComponentLoader } from "@/components/feedback/component-loader";
import NotFound from "@/components/feedback/not-found";
import type { Context } from "@/routes/__root";

export const Route = createFileRoute("/quizzes/$quizId")({
  loader,
  pendingMs: 100,
  pendingComponent: () => (
    <div className="flex flex-1 items-center justify-center p-4">
      <ComponentLoader />
    </div>
  ),
  notFoundComponent: () => <NotFound />,
  component: Outlet,
});

async function loader({
  context,
  params,
}: {
  context: Context;
  params: { quizId: string };
}) {
  try {
    const quiz = await context.queryClient.ensureQueryData(
      getQuizQueryOptions(params.quizId)
    );

    if (!quiz.isPublic) {
      throw notFound();
    }

    return { quiz };
  } catch (error) {
    if (
      axios.isAxiosError(error) &&
      (error.response?.status === 403 || error.response?.status === 404)
    ) {
      throw notFound();
    }

    throw error;
  }
}

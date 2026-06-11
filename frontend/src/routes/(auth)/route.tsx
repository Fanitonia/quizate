/* eslint-disable react-refresh/only-export-components */
import { Outlet, createFileRoute, redirect } from "@tanstack/react-router";

import { fetchCurrentUser } from "@/api/current-user";
import type { Context } from "@/routes/__root";

import { ComponentLoader, SomethingGoneWrong } from "@components/feedback";

export const Route = createFileRoute("/(auth)")({
  component: RouteComponent,
  loader,
  pendingMs: 100,
  pendingComponent: ComponentLoader,
  errorComponent: () => <SomethingGoneWrong />,
});

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Outlet />
    </div>
  );
}

async function loader({ context }: { context: Context }) {
  const currentUser = await fetchCurrentUser(context.queryClient);

  if (currentUser) {
    throw redirect({
      to: "/",
    });
  }
}

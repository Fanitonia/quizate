/* eslint-disable react-refresh/only-export-components */
import { Outlet, createFileRoute, redirect } from "@tanstack/react-router";

import { fetchCurrentUser } from "@/api/current-user";
import type { Context } from "@/routes/__root";

import { ComponentLoader } from "@components/feedback";

export const Route = createFileRoute("/(auth)")({
  component: RouteComponent,
  loader,
  pendingMs: 100,
  pendingComponent: ComponentLoader,
});

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Outlet />
    </div>
  );
}

async function loader({ context }: { context: Context }) {
  let currentUser;
  try {
    currentUser = await fetchCurrentUser(context.queryClient);
  } catch {
    console.error("Server error while fetching current user");
  }

  if (currentUser) {
    throw redirect({
      to: "/",
    });
  }
}

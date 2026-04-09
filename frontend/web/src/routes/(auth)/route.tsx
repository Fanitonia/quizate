/* eslint-disable react-refresh/only-export-components */
import { Outlet, createFileRoute, redirect } from "@tanstack/react-router";

import type { Context } from "@/routes/__root";

import { ensureCurrentUser } from "@api/current-user";

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
  try {
    const currentUser = await ensureCurrentUser(context.queryClient);

    if (currentUser) {
      throw redirect({
        to: "/",
      });
    }
  } catch {
    console.log("Server error during auth route loader.");
  }
}

import { createFileRoute, redirect } from "@tanstack/react-router";

import { ensureCurrentUserIfLoggedIn } from "@/api/user/currentUserQueryOptions";
import { ComponentLoader } from "@/components/feedback/component-loader";
import ProfilePage from "@/features/user/profile-page";
import type { Context } from "@/routes/__root";

export const Route = createFileRoute("/users/me")({
  loader,
  pendingMs: 100,
  pendingComponent: () => (
    <div className="flex flex-1 items-center justify-center p-4">
      <ComponentLoader />
    </div>
  ),
  component: ProfilePage,
});

async function loader({ context }: { context: Context }) {
  const currentUser = await ensureCurrentUserIfLoggedIn(context.queryClient);

  if (!currentUser) {
    throw redirect({
      to: "/login",
    });
  }
}

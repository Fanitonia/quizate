// EXTERNAL LIBRARIES
import { Outlet, createFileRoute, redirect } from "@tanstack/react-router";

// API & TYPES
import { ensureCurrentUserIfLoggedIn } from "@/api/auth/query-options";
import type { Context } from "@/routes/__root";

// COMPONENTS
import { ComponentLoader } from "@/components/component-loader";

export const Route = createFileRoute("/(auth)")({
  component: RouteComponent,
  loader,
  pendingMs: 100,
  pendingComponent: () => <ComponentLoader />,
});

async function loader({ context }: { context: Context }) {
  try {
    const currentUser = await ensureCurrentUserIfLoggedIn(context.queryClient);

    if (currentUser) {
      throw redirect({
        to: "/",
      });
    }
  } catch {
    console.log("Server error during auth route loader.");
  }
}

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Outlet />
    </div>
  );
}

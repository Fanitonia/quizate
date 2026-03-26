// EXTERNAL LIBRARIES
import { Outlet, createFileRoute, redirect } from "@tanstack/react-router";

// API & TYPES
import { getCurrentUserQueryOptions } from "@/api/auth/query-options";
import type { Context } from "@/routes/__root";

// COMPONENTS
import SomethingGoneWrong from "@/components/gone-wrong";
import { Spinner } from "@/components/ui/spinner";

export const Route = createFileRoute("/(auth)")({
  component: RouteComponent,
  loader,
  errorComponent: () => <SomethingGoneWrong />,
  pendingMs: 100,
  pendingComponent: () => <PendingComponent />,
});

async function loader({ context }: { context: Context }) {
  const currentUser = await context.queryClient.ensureQueryData(
    getCurrentUserQueryOptions()
  );

  if (currentUser) {
    throw redirect({
      to: "/",
    });
  }
}

function PendingComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Spinner className="size-8" />
    </div>
  );
}

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Outlet />
    </div>
  );
}

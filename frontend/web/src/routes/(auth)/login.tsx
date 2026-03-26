// EXTERNAL LIBRARIES
import { createFileRoute, redirect } from "@tanstack/react-router";
// API
import { getCurrentUserQueryOptions } from "@/api/auth/query-options";
// COMPONENTS
import LoginForm from "@/features/auth/login/login-form";

export const Route = createFileRoute("/(auth)/login")({
  component: RouteComponent,
  loader: async ({ context }) => {
    const currentUser = await context.queryClient.ensureQueryData(
      getCurrentUserQueryOptions()
    );

    if (currentUser) {
      throw redirect({
        to: "/",
      });
    }
  },
});

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <LoginForm className="w-90"></LoginForm>
    </div>
  );
}

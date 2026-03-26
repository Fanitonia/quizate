// EXTERNAL LIBRARIES
import { createFileRoute } from "@tanstack/react-router";

// COMPONENTS
import LoginForm from "@/features/auth/login/login-form";

export const Route = createFileRoute("/(auth)/login")({
  component: RouteComponent,
});

function RouteComponent() {
  return <LoginForm className="w-90"></LoginForm>;
}

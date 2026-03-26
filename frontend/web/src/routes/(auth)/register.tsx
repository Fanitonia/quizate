// EXTERNAL LIBRARIES
import { createFileRoute } from "@tanstack/react-router";

// COMPONENTS
import RegisterForm from "@/features/auth/register/register-form";

export const Route = createFileRoute("/(auth)/register")({
  component: RouteComponent,
});

function RouteComponent() {
  return <RegisterForm className="w-90"></RegisterForm>;
}

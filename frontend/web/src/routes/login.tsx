import LoginForm from "@/components/login-form";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/login")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <LoginForm className="w-90"></LoginForm>
    </div>
  );
}

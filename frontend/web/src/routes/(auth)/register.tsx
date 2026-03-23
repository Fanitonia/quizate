import { createFileRoute } from "@tanstack/react-router";
import RegisterForm from "@/components/register-form";

export const Route = createFileRoute("/(auth)/register")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <RegisterForm className="w-90"></RegisterForm>
    </div>
  );
}

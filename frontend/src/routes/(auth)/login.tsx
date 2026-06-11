/* eslint-disable react-refresh/only-export-components */
import { createFileRoute } from "@tanstack/react-router";

import { LoginCard } from "@/features/auth/components";

export const Route = createFileRoute("/(auth)/login")({
  component: RouteComponent,
});

function RouteComponent() {
  return <LoginCard></LoginCard>;
}

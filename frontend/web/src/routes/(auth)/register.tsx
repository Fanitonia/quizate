/* eslint-disable react-refresh/only-export-components */
import { createFileRoute } from "@tanstack/react-router";

import { RegisterCard } from "@/features/auth/components";

export const Route = createFileRoute("/(auth)/register")({
  component: RouteComponent,
});

function RouteComponent() {
  return <RegisterCard></RegisterCard>;
}

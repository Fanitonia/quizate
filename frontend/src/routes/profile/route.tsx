/* eslint-disable react-refresh/only-export-components */
import { Outlet, createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/profile")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="flex flex-1 justify-center">
      <Outlet />
    </div>
  );
}

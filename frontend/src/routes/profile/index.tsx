import type { QueryClient } from "@tanstack/react-query";
import { createFileRoute, redirect } from "@tanstack/react-router";

import { fetchCurrentUser } from "@api/current-user";

export const Route = createFileRoute("/profile/")({
  loader: ({ context }) => loader(context.queryClient),
});

async function loader(queryClient: QueryClient) {
  const currentUser = await fetchCurrentUser(queryClient);

  if (currentUser) {
    return redirect({
      to: "/profile/$username",
      params: { username: currentUser.username },
    });
  } else {
    return redirect({ to: "/login" });
  }
}

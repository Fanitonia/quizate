import { type QueryClient } from "@tanstack/react-query";
import {
  Outlet,
  createRootRouteWithContext,
  useMatches,
} from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

import { ensureCurrentUser } from "@api/current-user";

import { NotFound } from "@components/feedback";
import Footer from "@components/layout/footer";
import Navbar from "@components/layout/navbar";
import { Spinner } from "@components/ui/spinner";

declare module "@tanstack/react-router" {
  interface StaticDataRouteOption {
    hideNavFooter?: boolean;
  }
}

const RootLayout = () => {
  const hideNavFooter = useMatches({
    select: (matches) =>
      matches.some((match) => match.staticData?.hideNavFooter),
  });

  return (
    <>
      <div className="flex min-h-screen flex-1 flex-col">
        {!hideNavFooter && <Navbar />}
        <Outlet />
      </div>
      {!hideNavFooter && <Footer />}
      <TanStackRouterDevtools />
    </>
  );
};

type Context = {
  queryClient: QueryClient;
};

const Route = createRootRouteWithContext<Context>()({
  component: RootLayout,
  loader,
  pendingMs: 0,
  pendingComponent: () => <FullScreenLoader />,
  notFoundComponent: () => <NotFound />,
});

async function loader({ context }: { context: Context }) {
  try {
    await ensureCurrentUser(context.queryClient);
  } catch (error) {
    console.error("Server error during root loader.", error);
  }
}

function FullScreenLoader() {
  return (
    <div className="flex min-h-dvh flex-1 items-center justify-center p-4">
      <Spinner className="size-10" />
    </div>
  );
}

export { Route, type Context };

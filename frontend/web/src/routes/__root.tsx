// EXTERNAL LIBRARIES
import { type QueryClient } from "@tanstack/react-query";
import {
  createRootRouteWithContext,
  Outlet,
  useMatches,
} from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

// API
import { ensureCurrentUserIfLoggedIn } from "@/api/user/currentUserQueryOptions";

// COMPONENTS
import Footer from "@/components/footer";
import Navbar from "@/components/navbar";
import { ComponentLoader } from "@/components/feedback/component-loader";
import NotFound from "@/components/feedback/not-found";

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
  loader: async ({ context }) => {
    try {
      await ensureCurrentUserIfLoggedIn(context.queryClient);
    } catch (error) {
      console.error("Server error during root loader.");
    }
  },
  pendingMs: 0,
  pendingComponent: () => <FullScreenLoader />,
  notFoundComponent: () => <NotFound />,
});

function FullScreenLoader() {
  return (
    <div className="flex min-h-dvh flex-1 items-center justify-center p-4">
      <ComponentLoader />
    </div>
  );
}

export { Route, type Context };

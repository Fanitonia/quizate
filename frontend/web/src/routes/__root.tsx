// EXTERNAL LIBRARIES
import { useQuery, type QueryClient } from "@tanstack/react-query";
import {
  createRootRouteWithContext,
  Outlet,
  useMatches,
} from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

// API
import { getCurrentUserQueryOptions } from "@/api/auth/query-options";

// COMPONENTS
import Footer from "@/components/footer";
import Navbar from "@/components/navbar";

declare module "@tanstack/react-router" {
  interface StaticDataRouteOption {
    hideNavFooter?: boolean;
  }
}

const RootLayout = () => {
  useQuery(getCurrentUserQueryOptions());

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

export const Route = createRootRouteWithContext<Context>()({
  component: RootLayout,
});

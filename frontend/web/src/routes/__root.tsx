import Footer from "@/components/footer";
import Navbar from "@/components/navbar";
import { createRootRoute, Outlet, useMatches } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

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

export const Route = createRootRoute({ component: RootLayout });

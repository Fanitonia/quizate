import { createRootRoute, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import Navbar from "@/components/navbar";
import Footer from "@/components/footer";

const RootLayout = () => (
  <>
    <Navbar></Navbar>
    <div className="flex min-h-screen flex-col">
      <Outlet />
    </div>
    <Footer></Footer>
    <TanStackRouterDevtools />
  </>
);

export const Route = createRootRoute({ component: RootLayout });

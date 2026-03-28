// REACT
import { StrictMode, Suspense } from "react";
import ReactDOM from "react-dom/client";

// LIBRARIES
import { RouterProvider, createRouter } from "@tanstack/react-router";
import { routeTree } from "./routeTree.gen";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

// THEME
import { ThemeProvider } from "./stores/theme-provider";
import "./index.css";

// I18N
import "./utils/i18n";
import { ComponentLoader } from "./components/feedback/component-loader";

const queryClient = new QueryClient();

const router = createRouter({
  routeTree,
  context: { queryClient: queryClient },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <StrictMode>
      <Suspense fallback={<ComponentLoader />}>
        <ThemeProvider defaultTheme="dark">
          <QueryClientProvider client={queryClient}>
            <RouterProvider router={router} />
            <ReactQueryDevtools initialIsOpen={false} />
          </QueryClientProvider>
        </ThemeProvider>
      </Suspense>
    </StrictMode>
  );
}

import {
  queryOptions,
  useQuery,
  type QueryClient,
} from "@tanstack/react-query";

import { useUserStore } from "@/stores/user-store";

import { getCurrentUser } from "./auth-requests";

const currentUserQueryKey = ["currentUser"] as const;

function shouldFetchCurrentUser() {
  const { hasHydrated, isLoggedIn } = useUserStore.getState();

  return hasHydrated && isLoggedIn;
}

async function currentUserQueryFn() {
  const currentUser = await getCurrentUser();
  const { login, logout } = useUserStore.getState();

  if (currentUser) {
    login();
  } else {
    logout();
  }

  return currentUser;
}

function getCurrentUserQueryOptions() {
  return queryOptions({
    queryKey: currentUserQueryKey,
    queryFn: currentUserQueryFn,
    staleTime: 1 * 60 * 1000, // 1 minute
    retry: false,
  });
}

function useCurrentUserQuery() {
  const isLoggedIn = useUserStore((state) => state.isLoggedIn);
  const hasHydrated = useUserStore((state) => state.hasHydrated);

  return useQuery({
    ...getCurrentUserQueryOptions(),
    enabled: hasHydrated && isLoggedIn,
  });
}

async function ensureCurrentUserIfLoggedIn(queryClient: QueryClient) {
  if (!shouldFetchCurrentUser()) {
    return null;
  }

  return queryClient.ensureQueryData(getCurrentUserQueryOptions());
}

export {
  currentUserQueryKey,
  ensureCurrentUserIfLoggedIn,
  getCurrentUserQueryOptions,
  useCurrentUserQuery,
};

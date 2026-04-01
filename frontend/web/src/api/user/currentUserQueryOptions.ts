import {
  queryOptions,
  useQuery,
  type QueryClient,
} from "@tanstack/react-query";

import { useUserStore } from "@/stores/user-store";

import { getCurrentUser } from "./requests";

const currentUserQueryKey = ["currentUser"] as const;

function useCurrentUserQuery() {
  return useQuery({
    ...getCurrentUserQueryOptions(),
    enabled: shouldFetchCurrentUser(),
  });
}

async function ensureCurrentUserIfLoggedIn(queryClient: QueryClient) {
  if (!shouldFetchCurrentUser()) {
    return null;
  }

  return queryClient.ensureQueryData(getCurrentUserQueryOptions());
}

function shouldFetchCurrentUser() {
  const { hasHydrated, isLoggedIn } = useUserStore.getState();

  return hasHydrated && isLoggedIn;
}

async function getCurrentUserQueryFn() {
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
    queryFn: getCurrentUserQueryFn,
    staleTime: 1 * 60 * 1000, // 1 minute
    retry: false,
  });
}

export {
  currentUserQueryKey,
  ensureCurrentUserIfLoggedIn,
  getCurrentUserQueryOptions,
  useCurrentUserQuery,
};

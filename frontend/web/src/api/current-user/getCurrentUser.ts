import { QueryClient, queryOptions } from "@tanstack/react-query";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";

import { useUserStore } from "@stores/user-store";

import { type DetailedUserInfo } from "@type/api/users";

import { apiClient } from "@api/client";

import { currentUserQueryKeys } from "./";

async function getCurrentUser(): Promise<DetailedUserInfo | null> {
  try {
    const response = await apiClient.get<DetailedUserInfo>("/users/me");
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 401) {
      return null;
    }

    throw error;
  }
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

// hook
function useCurrentUserQuery() {
  return useQuery(getCurrentUserQueryOptions());
}

// functions for fetching in route loaders
function prefetchCurrentUser(queryClient: QueryClient) {
  if (!shouldFetchCurrentUser()) return Promise.resolve();

  return queryClient.prefetchQuery(getCurrentUserQueryOptions());
}

function fetchCurrentUser(queryClient: QueryClient) {
  if (!shouldFetchCurrentUser()) return Promise.resolve();

  return queryClient.fetchQuery(getCurrentUserQueryOptions());
}

function getCurrentUserQueryOptions() {
  return queryOptions({
    queryKey: currentUserQueryKeys.info,
    queryFn: getCurrentUserQueryFn,
    staleTime: 3 * 60 * 1000, // 3 minutes
    retry: false,
    enabled: shouldFetchCurrentUser(),
  });
}

function shouldFetchCurrentUser() {
  const { hasHydrated, isLoggedIn } = useUserStore.getState();

  return hasHydrated && isLoggedIn;
}

export {
  useCurrentUserQuery,
  getCurrentUserQueryOptions,
  prefetchCurrentUser,
  fetchCurrentUser,
};

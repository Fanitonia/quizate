import { queryOptions } from "@tanstack/react-query";
import { QueryClient, useQuery } from "@tanstack/react-query";
import axios from "axios";

import { useUserStore } from "@stores/user-store";

import { type DetailedUserInfo } from "@type/api/users";

import { apiClient } from "@api/client";

import { currentUserQueryKeys } from "./";

// API FUNCTIONS
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

// REACT QUERY
function useCurrentUserQuery() {
  return useQuery({
    ...getCurrentUserQueryOptions(),
    enabled: shouldFetchCurrentUser(),
  });
}

async function ensureCurrentUser(queryClient: QueryClient) {
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
    queryKey: currentUserQueryKeys.info,
    queryFn: getCurrentUserQueryFn,
    staleTime: 1 * 60 * 1000, // 1 minute
    retry: false,
  });
}

export { ensureCurrentUser, useCurrentUserQuery, getCurrentUserQueryOptions };

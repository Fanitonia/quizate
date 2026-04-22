import { QueryClient, queryOptions, useQuery } from "@tanstack/react-query";
import axios from "axios";

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

// hook
function useCurrentUserQuery() {
  return useQuery(getCurrentUserQueryOptions());
}

// functions for fetching in route loaders
function prefetchCurrentUser(queryClient: QueryClient): Promise<void> {
  return queryClient.prefetchQuery(getCurrentUserQueryOptions());
}

function fetchCurrentUser(
  queryClient: QueryClient
): Promise<DetailedUserInfo | null> {
  return queryClient.fetchQuery(getCurrentUserQueryOptions());
}

function invalidateAndFetchCurrentUser(
  queryClient: QueryClient
): Promise<DetailedUserInfo | null> {
  queryClient.invalidateQueries({ queryKey: currentUserQueryKeys.info });
  return fetchCurrentUser(queryClient);
}

function getCurrentUserQueryOptions() {
  return queryOptions({
    queryKey: currentUserQueryKeys.info,
    queryFn: getCurrentUser,
    staleTime: 5 * 60 * 1000,
    gcTime: 20 * 60 * 1000,
    retry: false,
    refetchOnWindowFocus: false,
  });
}

export {
  useCurrentUserQuery,
  getCurrentUserQueryOptions,
  prefetchCurrentUser,
  fetchCurrentUser,
  invalidateAndFetchCurrentUser,
};

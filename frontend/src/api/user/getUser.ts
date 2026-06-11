import { queryOptions, useQuery } from "@tanstack/react-query";

import type { UserInfo } from "@type/api/users";

import { apiClient } from "@api/client";

import { userQueryKeys } from "./queryKeys";

async function getUserByUsername(username: string): Promise<UserInfo> {
  try {
    const response = await apiClient.get<UserInfo>(
      `/users/username/${username}`
    );
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

function useGetUserByUsernameQuery(username: string, enabled: boolean = true) {
  return useQuery(getUserByUsernameQueryOptions(username, enabled));
}

// TODO: querykey factory
function getUserByUsernameQueryOptions(
  username: string,
  enabled: boolean = true
) {
  return queryOptions({
    queryKey: userQueryKeys.info(username),
    queryFn: () => getUserByUsername(username),
    staleTime: 1 * 60 * 1000, // 1 minute
    enabled,
    retry: false,
  });
}

export { useGetUserByUsernameQuery, getUserByUsernameQueryOptions };

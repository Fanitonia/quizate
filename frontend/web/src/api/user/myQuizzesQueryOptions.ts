import { queryOptions, useQuery } from "@tanstack/react-query";

import { useUserStore } from "@/stores/user-store";

import { getCurrentUserQuizzes } from "./requests";

const myQuizzesQueryKey = ["myQuizzes"] as const;

function useMyQuizzesQuery() {
  return useQuery({
    ...getMyQuizzesQueryOptions(),
    enabled: shouldFetchMyQuizzes(),
  });
}

function shouldFetchMyQuizzes() {
  const { hasHydrated, isLoggedIn } = useUserStore.getState();

  return hasHydrated && isLoggedIn;
}

function getMyQuizzesQueryOptions() {
  return queryOptions({
    queryKey: myQuizzesQueryKey,
    queryFn: getCurrentUserQuizzes,
    staleTime: 1 * 60 * 1000,
    retry: false,
  });
}

export { getMyQuizzesQueryOptions, myQuizzesQueryKey, useMyQuizzesQuery };

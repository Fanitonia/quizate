import { queryOptions } from "@tanstack/react-query";
import { getCurrentUser } from "./auth-requests";

function getCurrentUserQueryOptions() {
  return queryOptions({
    queryKey: ["currentUser"],
    queryFn: getCurrentUser,
    staleTime: 1 * 60 * 1000, // 1 minute
    retry: false,
  });
}

export { getCurrentUserQueryOptions };

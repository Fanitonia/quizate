import { useMutation } from "@tanstack/react-query";
import { useQueryClient } from "@tanstack/react-query";

import { useUserStore } from "@stores/user-store";

import { apiClient } from "@api/client";

import { currentUserQueryKeys } from "./";

async function logout() {
  return apiClient.post("/auth/logout");
}

function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logout,
    onSuccess: () => {
      useUserStore.getState().logout();
      queryClient.cancelQueries({ queryKey: currentUserQueryKeys.info });
      queryClient.setQueriesData({ queryKey: currentUserQueryKeys.info }, null);
    },
  });
}

export { logout, useLogout };

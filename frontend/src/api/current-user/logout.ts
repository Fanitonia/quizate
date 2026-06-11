import { useMutation } from "@tanstack/react-query";
import { useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";

import { apiClient } from "@api/client";

import { currentUserQueryKeys } from "./";

async function logout() {
  return apiClient.post("/auth/logout");
}

function useLogout() {
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: logout,
    onSuccess: () => {
      queryClient.cancelQueries({ queryKey: currentUserQueryKeys.info });
      queryClient.setQueryData(currentUserQueryKeys.info, null);
      navigate({ to: "/" });
    },
  });
}

export { logout, useLogout };

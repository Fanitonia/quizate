import { useMutation } from "@tanstack/react-query";
import { useQueryClient } from "@tanstack/react-query";

import { apiClient } from "@api/client";
import { currentUserQueryKeys } from "@api/current-user/queryKeys";

interface UpdateUserRequest {
  username?: string;
  displayName?: string;
  email?: string;
}

async function updateCurrentUser(request: UpdateUserRequest) {
  return apiClient.patch("/users/me", request);
}

function useUpdateCurrentUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateCurrentUser,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: currentUserQueryKeys.info });
    },
  });
}

export { updateCurrentUser, useUpdateCurrentUser, type UpdateUserRequest };

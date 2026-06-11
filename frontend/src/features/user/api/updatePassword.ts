import { useMutation } from "@tanstack/react-query";

import { apiClient } from "@api/client";

interface PasswordChangeRequest {
  oldPassword: string;
  newPassword: string;
}

async function updateCurrentUserPassword(request: PasswordChangeRequest) {
  return apiClient.post("/users/me/change-password", request);
}

function useUpdateCurrentUserPassword() {
  return useMutation({
    mutationFn: updateCurrentUserPassword,
  });
}

export {
  updateCurrentUserPassword,
  useUpdateCurrentUserPassword,
  type PasswordChangeRequest,
};

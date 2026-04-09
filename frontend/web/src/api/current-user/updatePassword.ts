import { apiClient } from "@api/client";

interface PasswordChangeRequest {
  oldPassword: string;
  newPassword: string;
}

async function updateCurrentUserPassword(request: PasswordChangeRequest) {
  return apiClient.post("/user/change-password", request);
}

export { updateCurrentUserPassword, type PasswordChangeRequest };

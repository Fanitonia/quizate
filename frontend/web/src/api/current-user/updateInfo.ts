import { apiClient } from "@api/client";

interface UpdateUserRequest {
  username?: string;
  email?: string;
}

async function updateCurrentUser(request: UpdateUserRequest) {
  return apiClient.patch("/users/me", request);
}

export { updateCurrentUser, type UpdateUserRequest };

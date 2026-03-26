interface UpdateUserRequest {
  username?: string;
  email?: string;
}

interface UpdateUserRoleRequest {
  role: "User" | "Admin";
}

interface UserInfoResponse {
  id: string;
  createdAt: string;
  username: string;
  normalizedUsername: string;
  profilePictureUrl: string | null;
  role: string;
}

interface DetailedUserInfoResponse extends UserInfoResponse {
  email: string | null;
  isEmailVerified: boolean;
}

export type {
  UpdateUserRequest,
  UpdateUserRoleRequest,
  UserInfoResponse as UserResponse,
  DetailedUserInfoResponse as DetailedUserResponse,
};

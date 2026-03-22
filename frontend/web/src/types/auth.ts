interface LoginRequest {
  usernameOrEmail: string;
  password: string;
}

interface RegisterRequest {
  username: string;
  email: string | null;
  password: string;
}

interface PasswordChangeRequest {
  oldPassword: string;
  newPassword: string;
}

export type { LoginRequest, RegisterRequest, PasswordChangeRequest };

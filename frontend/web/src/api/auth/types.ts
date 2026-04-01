interface LoginRequest {
  usernameOrEmail: string;
  password: string;
}

interface RegisterRequest {
  username: string;
  email: string | null;
  password: string;
}

export type { LoginRequest, RegisterRequest };

import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";
import type { AxiosError } from "axios";

import type { ErrorResponse } from "@type/api/error";

import { apiClient } from "@api/client";
import { invalidateAndFetchCurrentUser } from "@api/current-user";

interface LoginRequest {
  password: string;
  usernameOrEmail: string;
}

async function login(request: LoginRequest) {
  return apiClient.post("/auth/login", request);
}

function useLogin(onError?: (error: AxiosError<ErrorResponse>) => void) {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: LoginRequest) => login(data),
    onError: (error: AxiosError<ErrorResponse>) => onError?.(error),
    onSuccess: async () => {
      await invalidateAndFetchCurrentUser(queryClient);
      navigate({ to: "/", from: "/login" });
    },
  });
}

export { login, useLogin, type LoginRequest };

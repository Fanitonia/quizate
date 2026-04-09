import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";
import type { AxiosError } from "axios";

import { useUserStore } from "@/stores/user-store";
import type { ErrorResponse } from "@/types/api/error";

import { apiClient } from "@api/client";
import {
  currentUserQueryKeys,
  getCurrentUserQueryOptions,
} from "@api/current-user";

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
      useUserStore.getState().login();
      await queryClient.invalidateQueries({
        queryKey: currentUserQueryKeys.info,
      });
      await queryClient.prefetchQuery(getCurrentUserQueryOptions());
      navigate({ to: "/", from: "/login" });
    },
  });
}

export { login, useLogin, type LoginRequest };

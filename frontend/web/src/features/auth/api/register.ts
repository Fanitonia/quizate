import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";
import type { AxiosError } from "axios";

import { useUserStore } from "@stores/user-store";

import type { ErrorResponse } from "@type/api/error";

import { apiClient } from "@api/client";
import {
  currentUserQueryKeys,
  getCurrentUserQueryOptions,
} from "@api/current-user";

interface RegisterRequest {
  username: string;
  email: string | null;
  password: string;
}

async function register(request: RegisterRequest) {
  return apiClient.post("/auth/register", request);
}

function useRegister(onError?: (error: AxiosError<ErrorResponse>) => void) {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data: RegisterRequest) => register(data),
    onError: (error: AxiosError<ErrorResponse>) => onError?.(error),
    onSuccess: async () => {
      useUserStore.getState().login();
      await queryClient.invalidateQueries({
        queryKey: currentUserQueryKeys.info,
      });
      await queryClient.prefetchQuery(getCurrentUserQueryOptions());
      navigate({ to: "/", from: "/register" });
    },
  });
}

export { useRegister, type RegisterRequest };

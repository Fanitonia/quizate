import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";
import type { AxiosError } from "axios";

import type { ErrorResponse } from "@type/api/error";

import { apiClient } from "@api/client";
import { invalidateAndFetchCurrentUser } from "@api/current-user";

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
      await invalidateAndFetchCurrentUser(queryClient);
      navigate({ to: "/", from: "/register" });
    },
  });
}

export { useRegister, type RegisterRequest };

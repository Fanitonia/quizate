import axios from "axios";
import api from "../api";
import type { DetailedUserResponse } from "@/types/api/users";
import { type LoginRequest, type RegisterRequest } from "./auth-types";

async function login(request: LoginRequest) {
  return api.post("/auth/login", request);
}

async function logout() {
  return api.post("/auth/logout");
}

async function refresh() {
  return api.post("/auth/refresh-token");
}

async function register(request: RegisterRequest) {
  return api.post("/auth/register", request);
}

async function getCurrentUser(): Promise<DetailedUserResponse | null> {
  try {
    const response = await api.get<DetailedUserResponse>("/users/me");
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 401) {
      return null;
    }

    throw error;
  }
}

export { login, logout, refresh, register, getCurrentUser };

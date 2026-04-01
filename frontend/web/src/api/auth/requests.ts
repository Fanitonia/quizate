import api from "../apiClient";
import { type LoginRequest, type RegisterRequest } from "./types";

async function login(request: LoginRequest) {
  return api.post("/auth/login", request);
}

async function logout() {
  return api.post("/auth/logout");
}

async function register(request: RegisterRequest) {
  return api.post("/auth/register", request);
}

async function refresh() {
  return api.post("/auth/refresh-token");
}

export { login, logout, refresh, register };

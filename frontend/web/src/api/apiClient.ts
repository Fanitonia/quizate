// AI generated

import axios, { type AxiosError, type InternalAxiosRequestConfig } from "axios";

type RetryableRequestConfig = InternalAxiosRequestConfig & {
  _retry?: boolean;
};

const apiConfig = {
  baseURL: import.meta.env.VITE_API_URL,
  withCredentials: true,
};

const api = axios.create(apiConfig);
const refreshClient = axios.create(apiConfig);

let refreshRequest: Promise<void> | null = null;

function shouldRefresh(
  error: AxiosError,
  request?: RetryableRequestConfig
): request is RetryableRequestConfig {
  if (!request) {
    return false;
  }

  const status = error.response?.status;
  const url = request.url ?? "";

  const excludedPaths = [
    "/auth/login",
    "/auth/logout",
    "/auth/refresh-token",
    "/auth/register",
  ];

  return (
    status === 401 &&
    !request._retry &&
    !excludedPaths.some((path) => url.includes(path))
  );
}

async function refreshAccessToken() {
  if (!refreshRequest) {
    refreshRequest = refreshClient
      .post("/auth/refresh-token")
      .then(() => {})
      .finally(() => {
        refreshRequest = null;
      });
  }

  return refreshRequest;
}

api.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const request = error.config as RetryableRequestConfig | undefined;

    if (!shouldRefresh(error, request)) {
      return Promise.reject(error);
    }

    request._retry = true;

    try {
      await refreshAccessToken();
      return api(request);
    } catch (refreshError) {
      return Promise.reject(refreshError);
    }
  }
);

export default api;

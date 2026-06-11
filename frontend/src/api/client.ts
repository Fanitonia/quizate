import axios, { type AxiosError, type InternalAxiosRequestConfig } from "axios";

// Extend the Axios request config to include a _retry flag for tracking retry attempts
type RetryableRequestConfig = InternalAxiosRequestConfig & {
  _retry?: boolean;
};

const apiConfig = {
  baseURL: import.meta.env.VITE_API_URL,
  withCredentials: true,
};

const apiClient = axios.create(apiConfig);
const refreshClient = axios.create(apiConfig);

// Variable to track ongoing refresh token request to prevent multiple simultaneous refreshes
let refreshRequest: Promise<void> | null = null;

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

// Helper function to determine if the request should trigger a token refresh
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

// Response interceptor to handle token refresh on 401 errors
apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const request = error.config as RetryableRequestConfig | undefined;

    if (!shouldRefresh(error, request)) {
      return Promise.reject(error);
    }

    request._retry = true;

    try {
      await refreshAccessToken();
      return apiClient(request);
    } catch (refreshError) {
      return Promise.reject(refreshError);
    }
  }
);

export { apiClient };

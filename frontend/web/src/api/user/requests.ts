import axios from "axios";
import api from "../apiClient";
import type {
  DetailedUserInfoResponse,
  UpdateUserRequest,
  UserInfoResponse,
  PasswordChangeRequest,
} from "./types";
import type { QuizResponse } from "@/api/quiz/types";

async function getUser(userId: string): Promise<UserInfoResponse> {
  const response = await api.get<UserInfoResponse>(`/users/${userId}`);
  return response.data;
}

async function getUserQuizzes(userId: string): Promise<QuizResponse[]> {
  const response = await api.get<QuizResponse[]>(`/users/${userId}/quizzes`);
  return response.data;
}

async function getCurrentUser(): Promise<DetailedUserInfoResponse | null> {
  try {
    const response = await api.get<DetailedUserInfoResponse>("/users/me");
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 401) {
      return null;
    }

    throw error;
  }
}

async function getCurrentUserQuizzes(): Promise<QuizResponse[]> {
  const response = await api.get<QuizResponse[]>("/users/me/quizzes");

  return response.data;
}

async function updateCurrentUser(request: UpdateUserRequest) {
  return await api.patch("/users/me", request);
}

async function changeCurrentUserPassword(request: PasswordChangeRequest) {
  return await api.post("/users/me/change-password", request);
}

async function deleteCurrentUser() {
  return await api.delete("/users/me");
}

export {
  getUser,
  getUserQuizzes,
  getCurrentUser,
  getCurrentUserQuizzes,
  updateCurrentUser,
  changeCurrentUserPassword,
  deleteCurrentUser,
};

import api from "../apiClient";
import type {
  CreateTopicRequest,
  TopicResponse,
  UpdateTopicRequest,
} from "./types";

async function getTopics(): Promise<TopicResponse[]> {
  const response = await api.get<TopicResponse[]>("/topics");
  return response.data;
}

async function createTopic(
  request: CreateTopicRequest
): Promise<TopicResponse> {
  const response = await api.post<TopicResponse>("/topics", request);
  return response.data;
}

async function updateTopic(topicName: string, request: UpdateTopicRequest) {
  return await api.patch(`/topics/${topicName}`, request);
}

async function deleteTopic(topicName: string) {
  return await api.delete(`/topics/${topicName}`);
}

export { getTopics, createTopic, updateTopic, deleteTopic };

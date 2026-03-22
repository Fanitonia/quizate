interface TopicResponse {
  name: string;
  displayName: string;
  description: string | null;
  quizCount: number;
}

interface CreateTopicRequest {
  name: string | null;
  displayName: string | null;
  description: string | null;
}

interface UpdateTopicRequest {
  displayName: string | null;
  description: string | null;
}

export type { TopicResponse, CreateTopicRequest, UpdateTopicRequest };

import api from "../apiClient";
import type {
  CreateQuizRequest,
  GetQuizzesRequest,
  GetQuizzesResponse,
  QuizPagination,
  QuizQuestionsResponse,
  QuizResponse,
  UpdateQuizRequest,
} from "./types";

async function getQuizzes({
  page,
  pageSize,
}: GetQuizzesRequest): Promise<GetQuizzesResponse> {
  const response = await api.get<QuizResponse[]>("/quizzes", {
    params: {
      page,
      pageSize,
    },
  });

  return {
    quizzes: response.data,
    pagination: parseQuizPaginationHeader(
      getResponseHeaderValue(response.headers, "x-pagination"),
      {
        page,
        pageSize,
        resultCount: response.data.length,
      }
    ),
  };
}

async function getQuiz(quizId: string): Promise<QuizResponse> {
  const response = await api.get<QuizResponse>(`/quizzes/${quizId}`);
  return response.data;
}

async function createQuiz(request: CreateQuizRequest): Promise<QuizResponse> {
  const response = await api.post<QuizResponse>("/quizzes", request);
  return response.data;
}

async function updateQuiz(quizId: string, request: UpdateQuizRequest) {
  return await api.patch(`/quizzes/${quizId}`, request);
}

async function deleteQuiz(quizId: string) {
  return await api.delete(`/quizzes/${quizId}`);
}

async function getQuizQuestions(
  quizId: string
): Promise<QuizQuestionsResponse> {
  const response = await api.get<QuizQuestionsResponse>(
    `/quizzes/${quizId}/questions`
  );
  return response.data;
}

function parseQuizPaginationHeader(
  headerValue: string | string[] | null | undefined,
  fallback: {
    page: number;
    pageSize: number;
    resultCount: number;
  }
): QuizPagination {
  const fallbackPagination = createFallbackPagination(fallback);
  const rawHeader = Array.isArray(headerValue) ? headerValue[0] : headerValue;

  if (!rawHeader) {
    return fallbackPagination;
  }

  try {
    const parsedHeader = JSON.parse(rawHeader) as Record<string, unknown>;
    const currentPage =
      toPositiveInteger(
        getPaginationHeaderField(parsedHeader, "currentPage")
      ) ?? fallbackPagination.currentPage;
    const pageSize =
      toPositiveInteger(getPaginationHeaderField(parsedHeader, "pageSize")) ??
      fallbackPagination.pageSize;
    const totalCount =
      toNonNegativeInteger(
        getPaginationHeaderField(parsedHeader, "totalCount")
      ) ?? fallbackPagination.totalCount;
    const totalPages = Math.max(
      currentPage,
      toPositiveInteger(getPaginationHeaderField(parsedHeader, "totalPages")) ??
        Math.max(1, Math.ceil(totalCount / pageSize))
    );
    const hasPrevious = getPaginationHeaderField(parsedHeader, "hasPrevious");
    const hasNext = getPaginationHeaderField(parsedHeader, "hasNext");

    return {
      currentPage,
      pageSize,
      totalCount,
      totalPages,
      hasPrevious:
        typeof hasPrevious === "boolean" ? hasPrevious : currentPage > 1,
      hasNext:
        typeof hasNext === "boolean" ? hasNext : currentPage < totalPages,
    };
  } catch {
    return fallbackPagination;
  }
}

function getPaginationHeaderField(
  header: Record<string, unknown>,
  fieldName: string
) {
  const normalizedFieldName = fieldName.toLowerCase();

  for (const [key, value] of Object.entries(header)) {
    if (key.toLowerCase() === normalizedFieldName) {
      return value;
    }
  }

  return undefined;
}

function getResponseHeaderValue(
  headers: {
    get?: (headerName: string) => unknown;
    [key: string]: unknown;
  },
  headerName: string
) {
  const headerValue =
    (typeof headers.get === "function" ? headers.get(headerName) : null) ??
    headers[headerName] ??
    headers[headerName.toLowerCase()] ??
    headers[headerName.toUpperCase()];

  return typeof headerValue === "string" || Array.isArray(headerValue)
    ? headerValue
    : null;
}

function createFallbackPagination({
  page,
  pageSize,
  resultCount,
}: {
  page: number;
  pageSize: number;
  resultCount: number;
}): QuizPagination {
  const hasNext = resultCount === pageSize;
  const totalCount = hasNext
    ? page * pageSize + 1
    : (page - 1) * pageSize + resultCount;

  return {
    currentPage: page,
    pageSize,
    totalCount,
    totalPages: hasNext ? page + 1 : Math.max(1, page),
    hasPrevious: page > 1,
    hasNext,
  };
}

function toPositiveInteger(value: unknown) {
  return typeof value === "number" && Number.isInteger(value) && value > 0
    ? value
    : null;
}

function toNonNegativeInteger(value: unknown) {
  return typeof value === "number" && Number.isInteger(value) && value >= 0
    ? value
    : null;
}

export {
  getQuizzes,
  getQuiz,
  createQuiz,
  updateQuiz,
  deleteQuiz,
  getQuizQuestions,
};

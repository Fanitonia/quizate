import type { CreateQuizRequest, QuestionObject } from "@/api/quiz/types";

interface CreateQuizOptionFormValues {
  text: string;
  isCorrect: boolean;
}

interface CreateQuizQuestionFormValues {
  title: string;
  points: number;
  options: CreateQuizOptionFormValues[];
}

interface CreateQuizFormValues {
  title: string;
  description: string;
  isPublic: boolean;
  languageCode: string;
  topics: string;
  questions: CreateQuizQuestionFormValues[];
}

const DEFAULT_LANGUAGE_CODE = "en";

function createEmptyOption(
  overrides?: Partial<CreateQuizOptionFormValues>
): CreateQuizOptionFormValues {
  return {
    text: "",
    isCorrect: false,
    ...overrides,
  };
}

function createEmptyQuestion(
  overrides?: Partial<CreateQuizQuestionFormValues>
): CreateQuizQuestionFormValues {
  return {
    title: "",
    points: 10,
    options: [
      createEmptyOption({ isCorrect: true }),
      createEmptyOption({ isCorrect: false }),
    ],
    ...overrides,
  };
}

function getDefaultCreateQuizValues(): CreateQuizFormValues {
  return {
    title: "",
    description: "",
    isPublic: true,
    languageCode: DEFAULT_LANGUAGE_CODE,
    topics: "",
    questions: [createEmptyQuestion()],
  };
}

function normalizeNullableText(value: string): string | null {
  const normalizedValue = value.trim();
  return normalizedValue === "" ? null : normalizedValue;
}

function normalizeTopics(value: string): string[] {
  return [
    ...new Set(
      value
        .split(",")
        .map((topic) => topic.trim())
        .filter(Boolean)
    ),
  ];
}

function appendTopicValue(currentValue: string, topicName: string): string {
  const nextTopics = new Set(normalizeTopics(currentValue));

  nextTopics.add(topicName.trim());

  return Array.from(nextTopics).join(", ");
}

function toCreateQuizRequest(
  values: CreateQuizFormValues,
  questions: QuestionObject[]
): CreateQuizRequest {
  return {
    title: values.title.trim(),
    description: normalizeNullableText(values.description),
    thumbnailUrl: null,
    isPublic: values.isPublic,
    languageCode: values.languageCode.trim(),
    topics: normalizeTopics(values.topics),
    questions,
  };
}

export {
  DEFAULT_LANGUAGE_CODE,
  appendTopicValue,
  createEmptyOption,
  createEmptyQuestion,
  getDefaultCreateQuizValues,
  normalizeNullableText,
  normalizeTopics,
  toCreateQuizRequest,
};

export type {
  CreateQuizFormValues,
  CreateQuizOptionFormValues,
  CreateQuizQuestionFormValues,
};

// --- QUIZ ---
interface QuizResponse {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  title: string;
  description: string | null;
  thumbnailUrl: string | null;
  creatorId: string;
  creatorName: string;
  isPublic: boolean;
  languageCode: string;
  questionCount: number;
  attemptCount: number;
  topics: string[];
}

interface UpdateQuizRequest {
  title: string | null;
  description: string | null;
  thumbnailUrl: string | null;
  isPublic: boolean | null;
  languageCode: string | null;
}

interface CreateQuizRequest {
  title: string;
  description: string | null;
  thumbnailUrl: string | null;
  isPublic: boolean;
  languageCode: string;
  topics: string[];
  questions: QuestionObject[];
}

// --- QUESTIONS ---
const QUESTION_TYPES = {
  MultipleChoice: "multiple_choice",
};

type QuestionType = (typeof QUESTION_TYPES)[keyof typeof QUESTION_TYPES];

interface QuestionBase {
  type: QuestionType;
}

interface MultipleChoiceQuestionOption {
  text: string;
  imageUrl: string | null;
  displayOrder: number;
  isCorrect: boolean;
}

interface MultipleChoiceQuestionObject extends QuestionBase {
  type: typeof QUESTION_TYPES.MultipleChoice;
  title: string;
  imageUrl: string | null;
  points: number;
  options: MultipleChoiceQuestionOption[];
}

type QuestionObject = MultipleChoiceQuestionObject;

interface QuizQuestionsResponse {
  quizId: string;
  questions: QuestionObject[];
}

export { QUESTION_TYPES };

export type {
  QuizResponse,
  UpdateQuizRequest,
  CreateQuizRequest,
  QuestionType,
  QuestionBase,
  MultipleChoiceQuestionOption,
  MultipleChoiceQuestionObject,
  QuestionObject,
  QuizQuestionsResponse,
};

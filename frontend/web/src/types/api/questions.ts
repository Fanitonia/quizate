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

export type {
  QUESTION_TYPES,
  QuestionType,
  MultipleChoiceQuestionObject,
  MultipleChoiceQuestionOption,
  QuestionObject,
  QuizQuestionsResponse,
};

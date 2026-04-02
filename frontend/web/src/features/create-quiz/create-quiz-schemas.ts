import { z } from "zod";

import i18next from "@/utils/i18n";

const createQuizOptionSchema = z.object({
  text: z
    .string(i18next.t("createQuizPage.validation.optionTextRequired"))
    .trim()
    .min(1, i18next.t("createQuizPage.validation.optionTextRequired")),
  isCorrect: z.boolean(),
});

const createQuizQuestionSchema = z
  .object({
    title: z
      .string(i18next.t("createQuizPage.validation.questionTitleRequired"))
      .trim()
      .min(1, i18next.t("createQuizPage.validation.questionTitleRequired")),
    points: z
      .number(i18next.t("createQuizPage.validation.pointsRequired"))
      .min(1, i18next.t("createQuizPage.validation.pointsMin")),
    options: z
      .array(createQuizOptionSchema)
      .min(2, i18next.t("createQuizPage.validation.optionMin")),
  })
  .refine(
    (question) =>
      question.options.filter((option) => option.isCorrect).length === 1,
    {
      message: i18next.t("createQuizPage.validation.singleCorrect"),
      path: ["options"],
    }
  );

const createQuizFormSchema = z.object({
  title: z
    .string(i18next.t("createQuizPage.validation.titleRequired"))
    .trim()
    .min(1, i18next.t("createQuizPage.validation.titleRequired")),
  description: z.string(),
  isPublic: z.boolean(),
  languageCode: z
    .string(i18next.t("createQuizPage.validation.languageRequired"))
    .trim()
    .min(1, i18next.t("createQuizPage.validation.languageRequired")),
  topics: z.string(),
  questions: z
    .array(createQuizQuestionSchema)
    .min(1, i18next.t("createQuizPage.validation.questionMin")),
});

export { createQuizFormSchema };

import {
  DndContext,
  PointerSensor,
  closestCenter,
  useSensor,
  useSensors,
  type DragEndEvent,
} from "@dnd-kit/core";
import {
  SortableContext,
  useSortable,
  verticalListSortingStrategy,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link, useNavigate } from "@tanstack/react-router";
import type { AxiosError } from "axios";
import {
  Check,
  CirclePlus,
  GripVertical,
  LoaderCircle,
  Plus,
  Trash2,
} from "lucide-react";
import {
  FormProvider,
  useFieldArray,
  useForm,
  useFormContext,
  useWatch,
} from "react-hook-form";
import { useTranslation } from "react-i18next";

import { createQuiz } from "@/api/quiz/requests";
import {
  QUESTION_TYPES,
  type MultipleChoiceQuestionObject,
} from "@/api/quiz/types";
import { getTopics } from "@/api/topic/requests";
import AlertError from "@/components/feedback/alert-error";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import {
  Field,
  FieldDescription,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSet,
} from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Separator } from "@/components/ui/separator";
import type { ErrorResponse } from "@/types/api/error";
import { cn } from "@/lib/utils";

import { createQuizFormSchema } from "./create-quiz-schemas";
import {
  appendTopicValue,
  createEmptyOption,
  createEmptyQuestion,
  getDefaultCreateQuizValues,
  toCreateQuizRequest,
} from "./create-quiz-types";
import type { CreateQuizFormValues } from "./create-quiz-types";

const textareaClassName =
  "min-h-28 w-full rounded-lg border border-input bg-transparent px-3 py-2 text-sm outline-none transition-colors placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50 disabled:pointer-events-none disabled:cursor-not-allowed disabled:bg-input/50 disabled:opacity-50 aria-invalid:border-destructive aria-invalid:ring-3 aria-invalid:ring-destructive/20 dark:bg-input/30 dark:disabled:bg-input/80 dark:aria-invalid:border-destructive/50 dark:aria-invalid:ring-destructive/40";

function CreateQuizForm() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const form = useForm<CreateQuizFormValues>({
    resolver: zodResolver(createQuizFormSchema),
    defaultValues: getDefaultCreateQuizValues(),
  });

  const {
    control,
    getValues,
    handleSubmit,
    register,
    reset,
    setError,
    setValue,
    formState: { errors, isSubmitting },
  } = form;

  const {
    fields: questionFields,
    append: appendQuestion,
    move: moveQuestion,
    remove: removeQuestion,
  } = useFieldArray({
    control,
    name: "questions",
  });

  const questions = useWatch({ control, name: "questions" }) || [];
  const topicsValue = useWatch({ control, name: "topics" }) || "";
  const isPublic = useWatch({ control, name: "isPublic" });
  const languageCode = useWatch({ control, name: "languageCode" }) || "";
  const totalPoints = questions.reduce(
    (points, question) =>
      points + (Number.isFinite(question.points) ? question.points : 0),
    0
  );
  const totalOptions = questions.reduce(
    (optionCount, question) => optionCount + question.options.length,
    0
  );
  const readyQuestions = questions.filter((question) => {
    const correctOptions = question.options.filter(
      (option) => option.isCorrect
    ).length;

    return (
      question.title.trim() !== "" &&
      question.options.length >= 2 &&
      correctOptions === 1
    );
  }).length;

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 8,
      },
    })
  );

  const { data: topicSuggestions, isPending: isTopicsPending } = useQuery({
    queryKey: ["topics"],
    queryFn: getTopics,
  });

  const {
    mutateAsync: createQuizMutateAsync,
    isError,
    isPending,
  } = useMutation({
    mutationFn: createQuiz,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["quizzes"] });
      navigate({
        to: "/",
      });
    },
    onError: (error: AxiosError) => {
      setError("root", {
        message:
          (error.response?.data as ErrorResponse | undefined)?.description ||
          t("createQuizPage.error.description"),
      });
    },
  });

  const handleQuestionDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;

    if (!over || active.id === over.id) {
      return;
    }

    const oldIndex = questionFields.findIndex(
      (field) => field.id === active.id
    );
    const newIndex = questionFields.findIndex((field) => field.id === over.id);

    if (oldIndex === -1 || newIndex === -1) {
      return;
    }

    moveQuestion(oldIndex, newIndex);
  };

  const onSubmit = handleSubmit(async (values) => {
    const questionsPayload: MultipleChoiceQuestionObject[] =
      values.questions.map((question) => ({
        type: QUESTION_TYPES.MultipleChoice,
        title: question.title.trim(),
        imageUrl: null,
        points: question.points,
        options: question.options.map((option, index) => ({
          text: option.text.trim(),
          imageUrl: null,
          displayOrder: index,
          isCorrect: option.isCorrect,
        })),
      }));

    await createQuizMutateAsync(toCreateQuizRequest(values, questionsPayload));
  });

  return (
    <FormProvider {...form}>
      <form
        className="grid gap-6 xl:grid-cols-[minmax(0,1fr)_320px]"
        onSubmit={onSubmit}
      >
        <div className="space-y-6">
          <Card>
            <CardHeader>
              <CardTitle>{t("createQuizPage.metadata.title")}</CardTitle>
              <CardDescription>
                {t("createQuizPage.metadata.description")}
              </CardDescription>
            </CardHeader>
            <CardContent>
              <FieldSet>
                <FieldGroup>
                  <Field>
                    <FieldLabel htmlFor="quiz-title">
                      {t("createQuizPage.metadata.quizTitleLabel")}
                    </FieldLabel>
                    <Input id="quiz-title" {...register("title")} />
                    <FieldError errors={[errors.title]} />
                  </Field>
                  <Field>
                    <FieldLabel htmlFor="quiz-description">
                      {t("createQuizPage.metadata.descriptionLabel")}
                    </FieldLabel>
                    <textarea
                      id="quiz-description"
                      className={textareaClassName}
                      {...register("description")}
                    />
                    <FieldDescription>
                      {t("createQuizPage.metadata.descriptionHelp")}
                    </FieldDescription>
                  </Field>
                  <Field>
                    <FieldLabel htmlFor="quiz-language-code">
                      {t("createQuizPage.metadata.languageCodeLabel")}
                    </FieldLabel>
                    <Input
                      id="quiz-language-code"
                      {...register("languageCode")}
                    />
                    <FieldError errors={[errors.languageCode]} />
                  </Field>
                  <div className="grid gap-4 md:grid-cols-[minmax(0,1fr)_220px]">
                    <Field>
                      <FieldLabel htmlFor="quiz-topics">
                        {t("createQuizPage.metadata.topicsLabel")}
                      </FieldLabel>
                      <Input id="quiz-topics" {...register("topics")} />
                      <FieldDescription>
                        {t("createQuizPage.metadata.topicsHelp")}
                      </FieldDescription>
                    </Field>
                    <Field>
                      <FieldLabel>
                        {t("createQuizPage.metadata.visibilityLabel")}
                      </FieldLabel>
                      <div className="border-border bg-muted/40 flex gap-2 rounded-lg border p-1">
                        <Button
                          type="button"
                          variant={isPublic ? "default" : "ghost"}
                          className="flex-1"
                          onClick={() => {
                            setValue("isPublic", true, { shouldDirty: true });
                          }}
                        >
                          {t("createQuizPage.metadata.public")}
                        </Button>
                        <Button
                          type="button"
                          variant={!isPublic ? "default" : "ghost"}
                          className="flex-1"
                          onClick={() => {
                            setValue("isPublic", false, { shouldDirty: true });
                          }}
                        >
                          {t("createQuizPage.metadata.private")}
                        </Button>
                      </div>
                    </Field>
                  </div>
                  <Field>
                    <FieldLabel>
                      {t("createQuizPage.metadata.availableTopicsLabel")}
                    </FieldLabel>
                    <div className="border-border/80 bg-muted/20 flex min-h-10 flex-wrap gap-2 rounded-lg border border-dashed p-3">
                      {isTopicsPending && (
                        <span className="text-muted-foreground text-sm">
                          {t("createQuizPage.metadata.loadingTopics")}
                        </span>
                      )}
                      {!isTopicsPending && topicSuggestions?.length === 0 && (
                        <span className="text-muted-foreground text-sm">
                          {t("createQuizPage.metadata.noTopics")}
                        </span>
                      )}
                      {topicSuggestions?.map((topic) => {
                        const hasTopic = topicsValue
                          .split(",")
                          .map((value) => value.trim())
                          .includes(topic.name);

                        return (
                          <Button
                            key={topic.name}
                            type="button"
                            variant={hasTopic ? "secondary" : "outline"}
                            size="sm"
                            onClick={() => {
                              setValue(
                                "topics",
                                appendTopicValue(
                                  getValues("topics"),
                                  topic.name
                                ),
                                { shouldDirty: true }
                              );
                            }}
                          >
                            {topic.displayName}
                          </Button>
                        );
                      })}
                    </div>
                  </Field>
                </FieldGroup>
              </FieldSet>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="gap-3 md:flex md:flex-row md:items-center md:justify-between">
              <div className="space-y-1">
                <CardTitle>{t("createQuizPage.questions.title")}</CardTitle>
                <CardDescription>
                  {t("createQuizPage.questions.description")}
                </CardDescription>
              </div>
              <Button
                type="button"
                variant="outline"
                onClick={() => {
                  appendQuestion(createEmptyQuestion());
                }}
              >
                <CirclePlus />
                {t("createQuizPage.questions.addQuestion")}
              </Button>
            </CardHeader>
            <CardContent>
              <FieldError
                errors={[errors.questions as { message?: string } | undefined]}
                className="mb-4"
              />
              <DndContext
                sensors={sensors}
                collisionDetection={closestCenter}
                onDragEnd={handleQuestionDragEnd}
              >
                <SortableContext
                  items={questionFields.map((field) => field.id)}
                  strategy={verticalListSortingStrategy}
                >
                  <div className="space-y-4">
                    {questionFields.map((field, index) => (
                      <QuestionCard
                        key={field.id}
                        index={index}
                        questionCount={questionFields.length}
                        questionId={field.id}
                        removeQuestion={removeQuestion}
                      />
                    ))}
                  </div>
                </SortableContext>
              </DndContext>
            </CardContent>
          </Card>

          {isError && (
            <AlertError
              error={{
                title: t("createQuizPage.error.title"),
                description:
                  errors.root?.message || t("createQuizPage.error.description"),
              }}
            />
          )}
        </div>

        <div className="space-y-6 xl:sticky xl:top-6 xl:self-start">
          <Card>
            <CardHeader>
              <CardTitle>{t("createQuizPage.summary.title")}</CardTitle>
              <CardDescription>
                {t("createQuizPage.summary.description")}
              </CardDescription>
            </CardHeader>
            <CardContent className="space-y-4">
              <SummaryRow
                label={t("createQuizPage.summary.questionCount")}
                value={String(questionFields.length)}
              />
              <SummaryRow
                label={t("createQuizPage.summary.totalPoints")}
                value={String(totalPoints)}
              />
              <SummaryRow
                label={t("createQuizPage.summary.optionCount")}
                value={String(totalOptions)}
              />
              <SummaryRow
                label={t("createQuizPage.summary.readyQuestions")}
                value={`${readyQuestions}/${questionFields.length}`}
              />
              <SummaryRow
                label={t("createQuizPage.summary.visibility")}
                value={
                  isPublic
                    ? t("createQuizPage.metadata.public")
                    : t("createQuizPage.metadata.private")
                }
              />
              <SummaryRow
                label={t("createQuizPage.summary.language")}
                value={languageCode || "-"}
              />
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>{t("createQuizPage.actions.title")}</CardTitle>
              <CardDescription>
                {t("createQuizPage.actions.description")}
              </CardDescription>
            </CardHeader>
            <CardContent className="space-y-3">
              <Button
                type="submit"
                size="lg"
                className="w-full"
                disabled={isSubmitting}
              >
                {(isSubmitting || isPending) && (
                  <LoaderCircle className="animate-spin" />
                )}
                {t("createQuizPage.actions.submit")}
              </Button>
              <Button
                type="button"
                variant="outline"
                className="w-full"
                onClick={() => {
                  reset(getDefaultCreateQuizValues());
                }}
              >
                {t("createQuizPage.actions.reset")}
              </Button>
              <Link to="/" className="block">
                <Button type="button" variant="ghost" className="w-full">
                  {t("createQuizPage.actions.cancel")}
                </Button>
              </Link>
            </CardContent>
          </Card>
        </div>
      </form>
    </FormProvider>
  );
}

function QuestionCard({
  index,
  questionCount,
  questionId,
  removeQuestion,
}: {
  index: number;
  questionCount: number;
  questionId: string;
  removeQuestion: (index: number) => void;
}) {
  const { t } = useTranslation();
  const {
    control,
    formState: { errors },
    getValues,
    register,
    setValue,
  } = useFormContext<CreateQuizFormValues>();

  const {
    fields: optionFields,
    append: appendOption,
    move: moveOption,
    remove: removeOption,
  } = useFieldArray({
    control,
    name: `questions.${index}.options`,
  });

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 6,
      },
    })
  );

  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id: questionId,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  const handleOptionDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;

    if (!over || active.id === over.id) {
      return;
    }

    const oldIndex = optionFields.findIndex((field) => field.id === active.id);
    const newIndex = optionFields.findIndex((field) => field.id === over.id);

    if (oldIndex === -1 || newIndex === -1) {
      return;
    }

    moveOption(oldIndex, newIndex);
  };

  const markCorrectOption = (optionIndex: number) => {
    const options = getValues(`questions.${index}.options`);

    options.forEach((_, currentIndex) => {
      setValue(
        `questions.${index}.options.${currentIndex}.isCorrect`,
        currentIndex === optionIndex,
        {
          shouldDirty: true,
          shouldValidate: true,
        }
      );
    });
  };

  return (
    <section
      ref={setNodeRef}
      style={style}
      className={cn(
        "border-border/80 bg-background rounded-2xl border p-4 shadow-sm transition-shadow",
        isDragging && "ring-primary/20 shadow-lg ring-2"
      )}
    >
      <div className="flex flex-col gap-4">
        <div className="flex flex-col gap-3 md:flex-row md:items-start md:justify-between">
          <div className="space-y-1">
            <div className="text-muted-foreground text-sm font-medium">
              {t("createQuizPage.questions.questionLabel", {
                index: index + 1,
              })}
            </div>
            <p className="text-muted-foreground text-sm">
              {t("createQuizPage.questions.questionDescription")}
            </p>
          </div>
          <div className="flex items-center gap-2">
            <Button
              type="button"
              variant="ghost"
              size="icon-sm"
              {...attributes}
              {...listeners}
            >
              <GripVertical />
            </Button>
            <Button
              type="button"
              variant="destructive"
              size="icon-sm"
              disabled={questionCount === 1}
              onClick={() => {
                removeQuestion(index);
              }}
            >
              <Trash2 />
            </Button>
          </div>
        </div>

        <div className="grid gap-4 md:grid-cols-[minmax(0,1fr)_120px]">
          <Field>
            <FieldLabel htmlFor={`question-title-${questionId}`}>
              {t("createQuizPage.questions.promptLabel")}
            </FieldLabel>
            <Input
              id={`question-title-${questionId}`}
              {...register(`questions.${index}.title`)}
            />
            <FieldError errors={[errors.questions?.[index]?.title]} />
          </Field>
          <Field>
            <FieldLabel htmlFor={`question-points-${questionId}`}>
              {t("createQuizPage.questions.pointsLabel")}
            </FieldLabel>
            <Input
              id={`question-points-${questionId}`}
              type="number"
              min={1}
              step={1}
              {...register(`questions.${index}.points`, {
                valueAsNumber: true,
              })}
            />
            <FieldError errors={[errors.questions?.[index]?.points]} />
          </Field>
        </div>

        <Separator />

        <div className="space-y-4">
          <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
            <div>
              <h3 className="font-medium">
                {t("createQuizPage.options.title")}
              </h3>
              <p className="text-muted-foreground text-sm">
                {t("createQuizPage.options.description")}
              </p>
            </div>
            <Button
              type="button"
              variant="outline"
              onClick={() => {
                appendOption(createEmptyOption());
              }}
            >
              <Plus />
              {t("createQuizPage.options.addOption")}
            </Button>
          </div>

          <FieldError
            errors={[
              errors.questions?.[index]?.options as
                | { message?: string }
                | undefined,
            ]}
          />

          <DndContext
            sensors={sensors}
            collisionDetection={closestCenter}
            onDragEnd={handleOptionDragEnd}
          >
            <SortableContext
              items={optionFields.map((field) => field.id)}
              strategy={verticalListSortingStrategy}
            >
              <div className="space-y-3">
                {optionFields.map((field, optionIndex) => (
                  <OptionRow
                    key={field.id}
                    questionIndex={index}
                    optionIndex={optionIndex}
                    optionId={field.id}
                    canRemove={optionFields.length > 2}
                    markCorrectOption={markCorrectOption}
                    removeOption={removeOption}
                  />
                ))}
              </div>
            </SortableContext>
          </DndContext>
        </div>
      </div>
    </section>
  );
}

function OptionRow({
  questionIndex,
  optionIndex,
  optionId,
  canRemove,
  markCorrectOption,
  removeOption,
}: {
  questionIndex: number;
  optionIndex: number;
  optionId: string;
  canRemove: boolean;
  markCorrectOption: (optionIndex: number) => void;
  removeOption: (index: number) => void;
}) {
  const { t } = useTranslation();
  const {
    control,
    register,
    formState: { errors },
  } = useFormContext<CreateQuizFormValues>();

  const isCorrect = useWatch({
    control,
    name: `questions.${questionIndex}.options.${optionIndex}.isCorrect`,
  });

  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id: optionId,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      className={cn(
        "border-border/80 bg-muted/20 rounded-xl border p-3 transition-shadow",
        isDragging && "ring-primary/20 shadow-md ring-2"
      )}
    >
      <div className="flex flex-col gap-3 md:flex-row md:items-start">
        <div className="flex gap-2 md:pt-7">
          <Button
            type="button"
            variant="ghost"
            size="icon-sm"
            {...attributes}
            {...listeners}
          >
            <GripVertical />
          </Button>
        </div>
        <div className="flex-1">
          <Field>
            <FieldLabel htmlFor={`option-text-${optionId}`}>
              {t("createQuizPage.options.textLabel", {
                index: optionIndex + 1,
              })}
            </FieldLabel>
            <Input
              id={`option-text-${optionId}`}
              {...register(
                `questions.${questionIndex}.options.${optionIndex}.text`
              )}
            />
            <FieldError
              errors={[
                errors.questions?.[questionIndex]?.options?.[optionIndex]?.text,
              ]}
            />
          </Field>
        </div>
        <div className="flex flex-row gap-2 md:pt-7">
          <Button
            type="button"
            variant={isCorrect ? "default" : "outline"}
            className="min-w-32"
            onClick={() => {
              markCorrectOption(optionIndex);
            }}
          >
            <Check className={cn(!isCorrect && "opacity-0")} />
            {isCorrect
              ? t("createQuizPage.options.correct")
              : t("createQuizPage.options.markCorrect")}
          </Button>
          <Button
            type="button"
            variant="destructive"
            size="icon-sm"
            disabled={!canRemove}
            onClick={() => {
              removeOption(optionIndex);
            }}
          >
            <Trash2 />
          </Button>
        </div>
      </div>
    </div>
  );
}

function SummaryRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="border-border/70 bg-muted/20 flex items-center justify-between gap-3 rounded-xl border px-3 py-2">
      <span className="text-muted-foreground text-sm">{label}</span>
      <span className="font-medium">{value}</span>
    </div>
  );
}

export default CreateQuizForm;

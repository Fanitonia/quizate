import { useEffect, useState } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

import {
  getCurrentUserQueryOptions,
  currentUserQueryKey,
} from "@/api/user/currentUserQueryOptions";
import { updateCurrentUser } from "@/api/user/requests";
import AlertError from "@/components/feedback/alert-error";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Field,
  FieldContent,
  FieldDescription,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSet,
} from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Separator } from "@/components/ui/separator";
import { Spinner } from "@/components/ui/spinner";
import type { ErrorResponse } from "@/types/api/error";
import type { DetailedUserInfoResponse } from "@/api/user/types";

import {
  profileSettingsSchema,
  type ProfileSettingsFormFields,
} from "./profile-schemas";

function ProfileSettingsForm({
  currentUser,
  embedded = false,
}: {
  currentUser: DetailedUserInfoResponse;
  embedded?: boolean;
}) {
  const { t } = useTranslation();
  const queryClient = useQueryClient();
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    reset,
    clearErrors,
    setError,
    formState: { errors, isDirty, isSubmitting },
  } = useForm<ProfileSettingsFormFields>({
    resolver: zodResolver(profileSettingsSchema),
    defaultValues: {
      username: currentUser.username,
      email: currentUser.email ?? "",
    },
  });

  useEffect(() => {
    reset({
      username: currentUser.username,
      email: currentUser.email ?? "",
    });
  }, [currentUser.email, currentUser.username, reset]);

  const { mutateAsync: updateCurrentUserMutateAsync, isError } = useMutation({
    mutationFn: updateCurrentUser,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: currentUserQueryKey });
      await queryClient.prefetchQuery(getCurrentUserQueryOptions());
      setSuccessMessage(t("profilePage.settings.success"));
    },
    onError: (error: AxiosError) => {
      setSuccessMessage(null);
      setError("root", {
        message: (error.response?.data as ErrorResponse | undefined)
          ?.description,
      });
    },
  });

  const onSubmit: SubmitHandler<ProfileSettingsFormFields> = async (data) => {
    clearErrors("root");
    setSuccessMessage(null);

    await updateCurrentUserMutateAsync({
      username: data.username,
      email: data.email?.trim() || undefined,
    });

    reset({
      username: data.username,
      email: data.email?.trim() || "",
    });
  };

  const formContent = (
    <FieldSet>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <FieldGroup>
          <Field>
            <FieldLabel htmlFor="profile-username">{t("username")}</FieldLabel>
            <FieldContent>
              <Input
                {...register("username")}
                id="profile-username"
                autoComplete="username"
              />
              <FieldDescription>
                {t("profilePage.settings.usernameHelp")}
              </FieldDescription>
              <FieldError errors={[errors.username]} />
            </FieldContent>
          </Field>
          <Field>
            <FieldLabel htmlFor="profile-email">
              {t("email")} ({t("optional")})
            </FieldLabel>
            <FieldContent>
              <Input
                {...register("email")}
                id="profile-email"
                autoComplete="email"
                type="email"
              />
              <FieldDescription>
                {t("profilePage.settings.emailHelp")}
              </FieldDescription>
              <FieldError errors={[errors.email]} />
            </FieldContent>
          </Field>
        </FieldGroup>
        {isError && (
          <AlertError
            error={{
              title: t("profilePage.settings.error.title"),
              description:
                errors.root?.message ||
                t("profilePage.settings.error.description"),
            }}
          />
        )}
        <Separator />
        <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
          <div className="min-h-5 text-sm">
            {successMessage && (
              <p className="font-medium text-emerald-600 dark:text-emerald-400">
                {successMessage}
              </p>
            )}
          </div>
          <Button type="submit" size="lg" disabled={!isDirty || isSubmitting}>
            {isSubmitting && <Spinner />}
            {t("profilePage.settings.submit")}
          </Button>
        </div>
      </form>
    </FieldSet>
  );

  if (embedded) {
    return <div className="px-4 pb-4">{formContent}</div>;
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>{t("profilePage.settings.title")}</CardTitle>
        <CardDescription>
          {t("profilePage.settings.description")}
        </CardDescription>
      </CardHeader>
      <CardContent>{formContent}</CardContent>
    </Card>
  );
}

export default ProfileSettingsForm;

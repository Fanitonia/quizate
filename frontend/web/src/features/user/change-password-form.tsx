import { useState } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

import { changeCurrentUserPassword } from "@/api/user/requests";
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

import {
  changePasswordSchema,
  type ChangePasswordFormFields,
} from "./profile-schemas";

function ChangePasswordForm({ embedded = false }: { embedded?: boolean }) {
  const { t } = useTranslation();
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    reset,
    clearErrors,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<ChangePasswordFormFields>({
    resolver: zodResolver(changePasswordSchema),
    defaultValues: {
      oldPassword: "",
      newPassword: "",
      confirmPassword: "",
    },
  });

  const { mutateAsync: changePasswordMutateAsync, isError } = useMutation({
    mutationFn: changeCurrentUserPassword,
    onSuccess: () => {
      reset();
      setSuccessMessage(t("profilePage.password.success"));
    },
    onError: (error: AxiosError) => {
      setSuccessMessage(null);
      setError("root", {
        message: (error.response?.data as ErrorResponse | undefined)
          ?.description,
      });
    },
  });

  const onSubmit: SubmitHandler<ChangePasswordFormFields> = async (data) => {
    clearErrors("root");
    setSuccessMessage(null);

    await changePasswordMutateAsync({
      oldPassword: data.oldPassword,
      newPassword: data.newPassword,
    });
  };

  const formContent = (
    <FieldSet>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <FieldGroup>
          <Field>
            <FieldLabel htmlFor="current-password">
              {t("profilePage.password.currentPassword")}
            </FieldLabel>
            <FieldContent>
              <Input
                {...register("oldPassword")}
                id="current-password"
                type="password"
                autoComplete="current-password"
              />
              <FieldDescription>
                {t("profilePage.password.currentPasswordHelp")}
              </FieldDescription>
              <FieldError errors={[errors.oldPassword]} />
            </FieldContent>
          </Field>
          <Field>
            <FieldLabel htmlFor="new-password">
              {t("profilePage.password.newPassword")}
            </FieldLabel>
            <FieldContent>
              <Input
                {...register("newPassword")}
                id="new-password"
                type="password"
                autoComplete="new-password"
              />
              <FieldDescription>
                {t("profilePage.password.newPasswordHelp")}
              </FieldDescription>
              <FieldError errors={[errors.newPassword]} />
            </FieldContent>
          </Field>
          <Field>
            <FieldLabel htmlFor="confirm-password">
              {t("profilePage.password.confirmPassword")}
            </FieldLabel>
            <FieldContent>
              <Input
                {...register("confirmPassword")}
                id="confirm-password"
                type="password"
                autoComplete="new-password"
              />
              <FieldError errors={[errors.confirmPassword]} />
            </FieldContent>
          </Field>
        </FieldGroup>
        {isError && (
          <AlertError
            error={{
              title: t("profilePage.password.error.title"),
              description:
                errors.root?.message ||
                t("profilePage.password.error.description"),
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
          <Button type="submit" size="lg" disabled={isSubmitting}>
            {isSubmitting && <Spinner />}
            {t("profilePage.password.submit")}
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
        <CardTitle>{t("profilePage.password.title")}</CardTitle>
        <CardDescription>
          {t("profilePage.password.description")}
        </CardDescription>
      </CardHeader>
      <CardContent>{formContent}</CardContent>
    </Card>
  );
}

export default ChangePasswordForm;

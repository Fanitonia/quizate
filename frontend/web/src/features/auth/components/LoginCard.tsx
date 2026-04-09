import { zodResolver } from "@hookform/resolvers/zod";
import { Link } from "@tanstack/react-router";
import { type SubmitHandler, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

import { AlertError } from "@components/feedback";
import { Button } from "@components/ui/button";
import {
  Field,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSet,
} from "@components/ui/field";
import { Input } from "@components/ui/input";
import { Label } from "@components/ui/label";
import { Separator } from "@components/ui/separator";
import { Spinner } from "@components/ui/spinner";

import { useLogin } from "../api";
import { type LoginForm, loginFormSchema } from "../types";

function LoginCard() {
  // I18N
  const { t } = useTranslation();

  // FORM
  const {
    formState: { errors, isSubmitting },
    handleSubmit,
    register,
    setError,
  } = useForm({
    resolver: zodResolver(loginFormSchema),
  });

  // FORM SUBMISSION
  const onSubmit: SubmitHandler<LoginForm> = async (data) => {
    await loginMutateAsync(data);
  };

  // LOGIN API
  const { isError, mutateAsync: loginMutateAsync } = useLogin((error) => {
    setError("root", {
      message:
        error.response?.data.description ?? t("loginPage.error.description"),
    });
  });

  return (
    <div className="px-3 py-4 w-sm">
      <div className="flex flex-col items-center justify-center py-1 text-center mb-8">
        <h2>{t("loginPage.header.title")}</h2>
        <p className="text-muted-foreground">
          {t("loginPage.header.description")}
        </p>
      </div>
      <form noValidate onSubmit={handleSubmit(onSubmit)}>
        <FieldSet>
          <FieldGroup>
            <Field>
              <FieldLabel htmlFor="email-username">
                {t("loginPage.emailOrUsername")}
              </FieldLabel>
              <Input
                {...register("usernameOrEmail")}
                id="email-username"
              ></Input>
              <FieldError errors={[errors.usernameOrEmail]}></FieldError>
            </Field>
            <Field>
              <FieldLabel htmlFor="password">{t("password")}</FieldLabel>
              <Input
                {...register("password")}
                id="password"
                type="password"
              ></Input>
              <FieldError errors={[errors.password]}></FieldError>
            </Field>
          </FieldGroup>
          {isError && (
            <AlertError
              error={{
                description:
                  errors.root?.message || t("loginPage.error.description"),
                title: t("loginPage.error.title"),
              }}
            />
          )}
          <Separator className="my-2"></Separator>
          <Button
            className="w-full"
            disabled={isSubmitting}
            size="lg"
            type="submit"
          >
            {isSubmitting && <Spinner />}
            {t("login")}
          </Button>
          <div className="flex flex-row items-center justify-center">
            <Label
              className="text-accent-foreground/60 text-xs"
              htmlFor="signup"
            >
              {t("loginPage.footer.noAccount")}
            </Label>
            <Link to="/register">
              <Button
                className="text-accent-foreground/60 hover:text-foreground px-1 underline"
                id="signup"
                size="xs"
                variant="link"
              >
                {t("signup")}
              </Button>
            </Link>
          </div>
        </FieldSet>
      </form>
    </div>
  );
}

export { LoginCard };

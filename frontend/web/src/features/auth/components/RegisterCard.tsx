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

import { type RegisterRequest, useRegister } from "../api";
import { type RegisterForm, registerFormSchema } from "../types";

function RegisterCard() {
  // I18N
  const { t } = useTranslation();

  // FORM
  const {
    formState: { errors, isSubmitting },
    handleSubmit,
    register,
    setError,
  } = useForm({
    resolver: zodResolver(registerFormSchema),
  });

  // FORM SUBMISSION
  const onSubmit: SubmitHandler<RegisterForm> = async (data) => {
    await registerMutate(data as RegisterRequest);
  };

  // REGISTER API
  const { mutateAsync: registerMutate, isError } = useRegister((error) => {
    setError("root", {
      message:
        error.response?.data.description ?? t("registerPage.error.description"),
    });
  });

  return (
    <div className="min-w-min px-3 py-4 w-90">
      <div className="flex flex-col items-center justify-center py-1 text-center mb-8">
        <h2>{t("registerPage.header.title")}</h2>
        <p className="text-muted-foreground">
          {t("registerPage.header.description")}
        </p>
      </div>
      <div>
        <form noValidate onSubmit={handleSubmit(onSubmit)}>
          <FieldSet>
            <FieldGroup>
              <Field>
                <FieldLabel htmlFor="username">{t("username")}</FieldLabel>
                <Input
                  {...register("username")}
                  id="username"
                  type="text"
                  autoComplete="username"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.username]}
                ></FieldError>
              </Field>
              <Field>
                <FieldLabel htmlFor="email">
                  {t("email")}{" "}
                  <span className="text-muted-foreground">
                    ({t("optional")})
                  </span>
                </FieldLabel>
                <Input
                  {...register("email")}
                  id="email"
                  placeholder="quizate@example.com"
                  type="email"
                  autoComplete="email"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.email]}
                ></FieldError>
              </Field>
              <Field>
                <FieldLabel htmlFor="password">{t("password")}</FieldLabel>
                <Input
                  {...register("password")}
                  id="password"
                  type="password"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.password]}
                ></FieldError>
              </Field>
              <Field>
                <FieldLabel htmlFor="confirm-password">
                  {t("registerPage.confirmPassword")}
                </FieldLabel>
                <Input
                  {...register("confirmPassword")}
                  id="confirm-password"
                  type="password"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.confirmPassword]}
                ></FieldError>
              </Field>
            </FieldGroup>
            {isError && (
              <AlertError
                error={{
                  description:
                    errors.root?.message || t("registerPage.error.description"),
                  title: t("registerPage.error.title"),
                }}
              ></AlertError>
            )}
            <Separator className="my-2"></Separator>
            <Button
              className="w-full"
              disabled={isSubmitting}
              size="lg"
              type="submit"
            >
              {isSubmitting && <Spinner />}
              {t("signup")}
            </Button>
            <div className="flex flex-row items-center justify-center gap-0">
              <Label
                className="text-accent-foreground/60 text-xs"
                htmlFor="signup"
              >
                {t("registerPage.footer.haveAccount")}
              </Label>
              <Link to="/login">
                <Button
                  className="text-accent-foreground/60 hover:text-foreground px-1 underline"
                  id="signup"
                  size="xs"
                  variant="link"
                >
                  {t("login")}
                </Button>
              </Link>
            </div>
          </FieldSet>
        </form>
      </div>
    </div>
  );
}

export { RegisterCard };

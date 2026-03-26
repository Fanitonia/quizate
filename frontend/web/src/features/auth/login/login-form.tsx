// COMPONENTS & ICONS
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Field,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSet,
} from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { Spinner } from "@/components/ui/spinner";
import AlertError from "@/components/alert-error";

// EXTERNAL LIBRARIES
import { Link, useNavigate } from "@tanstack/react-router";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useForm, type SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import type { AxiosError } from "axios";

// UTILS
import { cn } from "@/lib/utils";

// API & TYPES
import { login } from "@/api/auth/auth-requests";
import type { LoginRequest } from "@/api/auth/auth-types";
import type { LoginFormFields } from "./login-types";
import { loginFormSchema } from "./login-schemas";
import {
  currentUserQueryKey,
  getCurrentUserQueryOptions,
} from "@/api/auth/query-options";
import { useUserStore } from "@/stores/user-store";
import type { ErrorResponse } from "@/types/api/error";

function LoginForm({ className }: { className?: string }) {
  const queryClient = useQueryClient();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm({
    resolver: zodResolver(loginFormSchema),
  });

  const navigate = useNavigate();

  const { mutateAsync: loginMutateAsync, isError } = useMutation({
    mutationFn: (data: LoginRequest) => login(data),
    onSuccess: async () => {
      useUserStore.getState().login();
      await queryClient.invalidateQueries({ queryKey: currentUserQueryKey });
      await queryClient.prefetchQuery(getCurrentUserQueryOptions());
      navigate({
        to: "/",
      });
    },
    onError: (error: AxiosError) => {
      setError("root", {
        message: (error.response?.data as ErrorResponse)?.description,
      });
    },
  });

  const onSubmit: SubmitHandler<LoginFormFields> = async (data) => {
    await loginMutateAsync(data);
  };

  return (
    <Card className={cn("min-w-min px-3 py-4", className)}>
      <LoginHeader />
      <CardContent>
        <FieldSet>
          <form onSubmit={handleSubmit(onSubmit)} className="gap-3">
            <FieldGroup>
              <Field>
                <FieldLabel htmlFor="email-username">
                  Email or Username
                </FieldLabel>
                <Input
                  {...register("usernameOrEmail")}
                  id="email-username"
                ></Input>
                <FieldError errors={[errors.usernameOrEmail]}></FieldError>
              </Field>
              <Field>
                <FieldLabel htmlFor="password">Password</FieldLabel>
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
                className="my-4"
                error={{
                  title: "Login Failed",
                  description:
                    errors.root?.message ||
                    "An error occurred during login. Please try again.",
                }}
              />
            )}
            <Separator className="my-4"></Separator>
            <Field>
              <Button
                size="lg"
                className="w-full"
                type="submit"
                disabled={isSubmitting}
              >
                {isSubmitting && <Spinner />}
                Login
              </Button>
            </Field>
          </form>
          <SignupFooter />
        </FieldSet>
      </CardContent>
    </Card>
  );
}

function LoginHeader() {
  return (
    <CardHeader className="flex flex-col items-center justify-center py-1 text-center">
      <CardTitle>Welcome back!</CardTitle>
      <CardDescription>Login to Quizate</CardDescription>
    </CardHeader>
  );
}

function SignupFooter() {
  return (
    <Field>
      <div className="flex flex-row items-center justify-center gap-0">
        <Label htmlFor="signup" className="text-accent-foreground/60 text-xs">
          Don't have an account?
        </Label>
        <Link to="/register">
          <Button
            id="signup"
            variant="link"
            size="xs"
            className="text-accent-foreground/60 hover:text-foreground px-1 underline"
          >
            Signup
          </Button>
        </Link>
      </div>
    </Field>
  );
}

export default LoginForm;

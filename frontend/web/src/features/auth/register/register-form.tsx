// EXTERNAL LIBRARIES
import { Link, useNavigate } from "@tanstack/react-router";
import { useForm, type SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";

// UTILS
import { cn } from "@/lib/utils";

// COMPONENTS
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
import AlertError from "@/components/feedback/alert-error";

// API / TYPES
import type { RegisterFormFields } from "./register-types";
import { registerFormSchema } from "./register-schemas";
import type { RegisterRequest } from "@/api/auth/auth-types";
import { register as registerRequest } from "@/api/auth/auth-requests";
import {
  currentUserQueryKey,
  getCurrentUserQueryOptions,
} from "@/api/auth/query-options";
import type { AxiosError } from "axios";
import type { ErrorResponse } from "@/types/api/error";

// STORES
import { useUserStore } from "@/stores/user-store";

function RegisterForm({ className }: { className?: string }) {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm({
    resolver: zodResolver(registerFormSchema),
  });

  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutateAsync: registerMutate, isError } = useMutation({
    mutationFn: async (data: RegisterRequest) => registerRequest(data),
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

  const onSubmit: SubmitHandler<RegisterFormFields> = async (data) => {
    await registerMutate(data as RegisterRequest);
  };

  return (
    <Card className={cn("min-w-min px-3 py-4", className)}>
      <RegisterHeader />
      <CardContent>
        <FieldSet>
          {/* USERNAME */}
          <form noValidate onSubmit={handleSubmit(onSubmit)}>
            <FieldGroup>
              <Field>
                <FieldLabel htmlFor="username">Username</FieldLabel>
                <Input
                  {...register("username")}
                  id="username"
                  type="text"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.username]}
                ></FieldError>
              </Field>
              {/* EMAIL */}
              <Field>
                <FieldLabel htmlFor="email">
                  Email{" "}
                  <span className="text-muted-foreground">(optional)</span>
                </FieldLabel>
                <Input
                  {...register("email")}
                  id="email"
                  placeholder="quizate@example.com"
                  type="email"
                ></Input>
                <FieldError
                  className="text-sm"
                  errors={[errors.email]}
                ></FieldError>
              </Field>
              {/* PASSWORD */}
              <Field>
                <FieldLabel htmlFor="password">Password</FieldLabel>
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
                  Confirm Password
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
                className="my-4"
                error={{
                  title: "Registration Failed",
                  description:
                    errors.root?.message ||
                    "An error occurred during registration. Please try again.",
                }}
              ></AlertError>
            )}
            <Separator className="my-4"></Separator>
            <Field>
              {/* REGISTER */}
              <Button
                size="lg"
                className="w-full"
                type="submit"
                disabled={isSubmitting}
              >
                {isSubmitting && <Spinner />}
                Register
              </Button>
            </Field>
          </form>
          <LoginFooter />
        </FieldSet>
      </CardContent>
    </Card>
  );
}

function RegisterHeader() {
  return (
    <CardHeader className="flex flex-col items-center justify-center py-1 text-center">
      <CardTitle>Create Your Account</CardTitle>
      <CardDescription>
        Enter your details below to get started.
      </CardDescription>
    </CardHeader>
  );
}

function LoginFooter() {
  return (
    <Field>
      <div className="flex flex-row items-center justify-center gap-0">
        <Label htmlFor="signup" className="text-accent-foreground/60 text-xs">
          Do you have an account?
        </Label>
        <Link to="/login">
          <Button
            id="signup"
            variant="link"
            size="xs"
            className="text-accent-foreground/60 hover:text-foreground px-1 underline"
          >
            Login
          </Button>
        </Link>
      </div>
    </Field>
  );
}

export default RegisterForm;

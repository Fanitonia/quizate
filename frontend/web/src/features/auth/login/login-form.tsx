// COMPONENTS & ICONS
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
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
import { CircleAlert } from "lucide-react";

// EXTERNAL LIBRARIES
import { Link, useNavigate } from "@tanstack/react-router";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useForm, type SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

// UTILS
import { cn } from "@/lib/utils";

// API & TYPES
import { login } from "@/api/auth/auth-requests";
import type { LoginRequest } from "@/api/auth/auth-types";
import type { LoginFormFields } from "./login-types";
import { loginFormSchema } from "./login-schemas";
import { getCurrentUserQueryOptions } from "@/api/auth/query-options";

function LoginForm({ className }: { className?: string }) {
  const queryClient = useQueryClient();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm({
    resolver: zodResolver(loginFormSchema),
  });

  const navigate = useNavigate();

  const { mutateAsync: loginMutateAsync, isError } = useMutation({
    mutationFn: (data: LoginRequest) => login(data),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["currentUser"] });
      await queryClient.prefetchQuery(getCurrentUserQueryOptions());
      navigate({
        to: "/",
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
            {isError && <LoginError className="my-4" />}
            <Separator className="my-4"></Separator>
            <Field>
              <Button
                size="lg"
                className="w-full"
                type="submit"
                disabled={isSubmitting}
              >
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

function LoginError({ className }: { className?: string }) {
  return (
    <Alert variant="destructive" className={className}>
      <CircleAlert />
      <AlertTitle>Login Failed</AlertTitle>
      <AlertDescription>
        User with the provided credentials does not exist or the password is
        incorrect.
      </AlertDescription>
    </Alert>
  );
}

export default LoginForm;

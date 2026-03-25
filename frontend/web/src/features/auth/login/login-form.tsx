import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Field, FieldError, FieldLabel, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { Link } from "@tanstack/react-router";
import { useActionState } from "react";
import { loginAction } from "./login-action";

function LoginForm({ className }: { className?: string }) {
  const [state, action, isPending] = useActionState(loginAction, {
    errors: null,
    form: null,
  });

  return (
    <Card className={cn("min-w-min px-3 py-4", className)}>
      <CardHeader className="flex flex-col items-center justify-center py-1 text-center">
        <CardTitle>Welcome back!</CardTitle>
        <CardDescription>Login to Quizate</CardDescription>
      </CardHeader>
      <CardContent>
        <form action={action}>
          <FieldSet>
            <Field>
              <FieldLabel htmlFor="email-username">
                Email or Username
              </FieldLabel>
              <Input
                id="email-username"
                name="email-username"
                key={state.form?.["email-username"] || ""}
                defaultValue={state.form?.["email-username"] || ""}
              ></Input>
              <FieldError
                errors={state?.errors?.fieldErrors["email-username"]?.map(
                  (error) => ({ message: error })
                )}
              ></FieldError>
            </Field>
            <Field>
              <FieldLabel htmlFor="password">Password</FieldLabel>
              <Input
                id="password"
                name="password"
                type="password"
                key={state.form?.password || ""}
                defaultValue={state.form?.password || ""}
              ></Input>
              <FieldError
                errors={state?.errors?.fieldErrors["password"]?.map(
                  (error) => ({
                    message: error,
                  })
                )}
              ></FieldError>
            </Field>
            <Separator></Separator>
            <Field>
              <Button
                size="lg"
                className="w-full"
                type="submit"
                disabled={isPending}
              >
                Login
              </Button>
              <div className="flex flex-row items-center justify-center gap-0">
                <Label
                  htmlFor="signup"
                  className="text-accent-foreground/60 text-xs"
                >
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
          </FieldSet>
        </form>
      </CardContent>
    </Card>
  );
}

export default LoginForm;

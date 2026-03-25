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
import { registerAction } from "./register-action";

function RegisterForm({ className }: { className?: string }) {
  const [state, action, isPending] = useActionState(registerAction, {
    errors: null,
    form: null,
  });

  return (
    <Card className={cn("min-w-min px-3 py-4", className)}>
      {/* TITLE */}
      <CardHeader className="flex flex-col items-center justify-center py-1 text-center">
        <CardTitle>Create Your Account</CardTitle>
        <CardDescription>
          Enter your details below to get started.
        </CardDescription>
      </CardHeader>
      {/* FORM */}
      <CardContent>
        <form noValidate action={action}>
          <FieldSet>
            {/* USERNAME */}
            <Field>
              <FieldLabel htmlFor="username">Username</FieldLabel>
              <Input
                id="username"
                name="username"
                type="text"
                defaultValue={state.form?.username || ""}
              ></Input>
              <FieldError
                errors={state.errors?.fieldErrors.username?.map((error) => ({
                  message: error,
                }))}
                className="text-sm"
              ></FieldError>
            </Field>
            {/* EMAIL */}
            <Field>
              <FieldLabel htmlFor="email">
                Email <span className="text-muted-foreground">(optional)</span>
              </FieldLabel>
              <Input
                id="email"
                name="email"
                placeholder="quizate@example.com"
                type="email"
                defaultValue={state.form?.email || ""}
              ></Input>
              <FieldError
                errors={state.errors?.fieldErrors.email?.map((error) => ({
                  message: error,
                }))}
              ></FieldError>
            </Field>
            {/* PASSWORD */}
            <Field>
              <FieldLabel htmlFor="password">Password</FieldLabel>
              <Input
                id="password"
                name="password"
                type="password"
                defaultValue={state.form?.password || ""}
              ></Input>
              <FieldError
                errors={state.errors?.fieldErrors.password?.map((error) => ({
                  message: error,
                }))}
              ></FieldError>
            </Field>
            <Field>
              <FieldLabel htmlFor="confirm-password">
                Confirm Password
              </FieldLabel>
              <Input
                id="confirm-password"
                name="confirm-password"
                type="password"
                defaultValue={state.form?.confirmPassword || ""}
              ></Input>
              <FieldError
                errors={state.errors?.fieldErrors.confirmPassword?.map(
                  (error) => ({
                    message: error,
                  })
                )}
              ></FieldError>
            </Field>
            <Separator></Separator>
            <Field>
              {/* REGISTER */}
              <Button
                size="lg"
                className="w-full"
                type="submit"
                disabled={isPending}
              >
                Register
              </Button>
              {/* LOGIN */}
              <div className="flex flex-row items-center justify-center gap-0">
                <Label
                  htmlFor="signup"
                  className="text-accent-foreground/60 text-xs"
                >
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
          </FieldSet>
        </form>
      </CardContent>
    </Card>
  );
}

export default RegisterForm;

import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
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
  FieldLegend,
  FieldSeparator,
  FieldSet,
  FieldTitle,
} from "@/components/ui/field";

import { Input } from "@/components/ui/input";
import { Button } from "./ui/button";
import { Label } from "./ui/label";
import { Separator } from "./ui/separator";
import { cn } from "@/lib/utils";
import { Link } from "@tanstack/react-router";

interface LoginFormProps {
  className?: string;
}

function LoginForm({ className }: LoginFormProps) {
  function handleSubmit(e: FormData) {
    var errors: { usernameOrEmail?: string[]; password?: string[] } = {
      usernameOrEmail: [],
      password: [],
    };

    var emailUsername = e.get("email-username");
    var password = e.get("password");

    console.log({ emailUsername, password });
  }

  return (
    <Card className={cn("min-w-min px-3 py-4", className)}>
      <CardHeader className="flex flex-col items-center justify-center py-1">
        <CardTitle>Welcome back!</CardTitle>
        <CardDescription>Login to Quizate</CardDescription>
      </CardHeader>
      <CardContent>
        <form action={handleSubmit}>
          <FieldSet>
            <Field>
              <FieldLabel htmlFor="email-username">
                Email or Username
              </FieldLabel>
              <Input id="email-username" name="email-username"></Input>
            </Field>
            <Field>
              <FieldLabel htmlFor="password">Password</FieldLabel>
              <Input id="password" name="password" type="password"></Input>
            </Field>
            <Separator></Separator>
            <Field>
              <Button size="lg" className="w-full" type="submit">
                Login
              </Button>
              <div className="flex flex-row items-center justify-center gap-0">
                <Label
                  htmlFor="signup"
                  className="text-accent-foreground/60 text-xs"
                >
                  Don't have an account?
                </Label>
                <Button
                  id="signup"
                  variant="link"
                  size="xs"
                  className="text-accent-foreground/60 hover:text-foreground px-1 underline"
                >
                  <Link to="/register">Signup</Link>
                </Button>
              </div>
            </Field>
          </FieldSet>
        </form>
      </CardContent>
    </Card>
  );
}

export default LoginForm;

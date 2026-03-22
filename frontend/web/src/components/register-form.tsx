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

interface RegisterFormProps {
  className?: string;
}

function RegisterForm({ className }: RegisterFormProps) {
  return (
    <Card className={cn("min-w-min px-3 py-4 text-center", className)}>
      {/* TITLE */}
      <CardHeader className="flex flex-col items-center justify-center py-1">
        <CardTitle>Create Your Account</CardTitle>
        <CardDescription>
          Enter your details below to get started.
        </CardDescription>
      </CardHeader>
      {/* FORM */}
      <CardContent>
        <form noValidate>
          <FieldSet>
            {/* USERNAME */}
            <Field>
              <FieldLabel htmlFor="username">Username</FieldLabel>
              <Input id="username" type="text"></Input>
              <FieldError></FieldError>
            </Field>
            {/* EMAIL */}
            <Field>
              <FieldLabel htmlFor="email">
                Email <span className="text-muted-foreground">(optional)</span>
              </FieldLabel>
              <Input
                id="email"
                placeholder="quizate@example.com"
                type="email"
              ></Input>
              <FieldError></FieldError>
            </Field>
            {/* PASSWORD */}
            <Field>
              <FieldLabel htmlFor="password">Password</FieldLabel>
              <Input id="password" type="password"></Input>
              <FieldError></FieldError>
            </Field>
            <Separator></Separator>
            <Field>
              {/* REGISTER */}
              <Button size="lg" className="w-full" type="submit">
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
                <Button
                  id="signup"
                  variant="link"
                  size="xs"
                  className="text-accent-foreground/60 hover:text-foreground px-1 underline"
                >
                  <Link to="/login">Login</Link>
                </Button>
              </div>
            </Field>
          </FieldSet>
        </form>
      </CardContent>
    </Card>
  );
}

export default RegisterForm;

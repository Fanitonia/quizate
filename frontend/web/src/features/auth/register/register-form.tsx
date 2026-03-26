// EXTERNAL LIBRARIES
import { Link } from "@tanstack/react-router";

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
import { Field, FieldError, FieldLabel, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";

function RegisterForm({ className }: { className?: string }) {
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
        <form noValidate>
          <FieldSet>
            {/* USERNAME */}
            <Field>
              <FieldLabel htmlFor="username">Username</FieldLabel>
              <Input id="username" name="username" type="text"></Input>
              <FieldError className="text-sm"></FieldError>
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
              ></Input>
              <FieldError></FieldError>
            </Field>
            {/* PASSWORD */}
            <Field>
              <FieldLabel htmlFor="password">Password</FieldLabel>
              <Input id="password" name="password" type="password"></Input>
              <FieldError></FieldError>
            </Field>
            <Field>
              <FieldLabel htmlFor="confirm-password">
                Confirm Password
              </FieldLabel>
              <Input
                id="confirm-password"
                name="confirm-password"
                type="password"
              ></Input>
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

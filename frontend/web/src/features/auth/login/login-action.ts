import { z } from "zod";
import { type LoginForm } from "./login-types";
import { loginFormSchema } from "./login-schemas";

type FormState = {
  errors: z.core.$ZodFlattenedError<LoginForm> | null;
  form: LoginForm | null;
};

async function loginAction(
  _: FormState,
  formData: FormData
): Promise<FormState> {
  const data: LoginForm = {
    "email-username": formData.get("email-username") as string,
    password: formData.get("password") as string,
  };

  const result = loginFormSchema.safeParse(data);

  if (!result.success) {
    return { errors: z.flattenError(result.error), form: data };
  }

  // TODO: api isteği atılmalı

  return { errors: null, form: data };
}

export { loginAction };

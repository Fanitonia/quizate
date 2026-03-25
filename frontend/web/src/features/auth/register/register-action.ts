import { z } from "zod";
import { type RegisterForm } from "./register-types";
import { registerFormSchema } from "./register-schemas";

type FormState = {
  errors: z.core.$ZodFlattenedError<RegisterForm> | null;
  form: RegisterForm | null;
};

async function registerAction(
  _: FormState,
  formData: FormData
): Promise<FormState> {
  const data: RegisterForm = {
    username: formData.get("username") as string,
    email: formData.get("email") as string,
    password: formData.get("password") as string,
    confirmPassword: formData.get("confirm-password") as string,
  };

  const result = registerFormSchema.safeParse(data);

  if (!result.success) {
    return { errors: z.flattenError(result.error), form: data };
  }

  // TODO: api isteği atılmalı

  return { errors: null, form: data };
}

export { registerAction };

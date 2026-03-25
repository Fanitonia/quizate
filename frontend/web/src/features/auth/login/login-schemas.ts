import { z } from "zod";
import type { LoginForm } from "./login-types";

const loginFormSchema: z.ZodType<LoginForm> = z.object({
  "email-username": z.string().min(1, "Email or username is required"),
  password: z.string().min(1, "Password is required"),
});

export { loginFormSchema };

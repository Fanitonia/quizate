import { z } from "zod";

const loginFormSchema = z.object({
  usernameOrEmail: z.string().min(1, "Email or username is required"),
  password: z.string().min(1, "Password is required"),
});

export { loginFormSchema };

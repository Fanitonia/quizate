import { z } from "zod";
import type { RegisterForm } from "./register-types";

const registerFormSchema: z.ZodType<RegisterForm> = z
  .object({
    username: z
      .string("Username is required")
      .min(3, "Username must be at least 3 characters long")
      .regex(
        /^[A-Za-z0-9_]+$/,
        "Username must be alphanumeric with underscores only"
      ),

    email: z.preprocess((value) => {
      if (typeof value !== "string") {
        return value;
      }
      const trimmedValue = value.trim();
      return trimmedValue === "" ? undefined : trimmedValue;
    }, z.email("Invalid email address").optional()),

    password: z
      .string("Password is required")
      .min(8, "Password must be at least 8 characters long")
      .regex(
        /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$/,
        "Password must contain at least one uppercase letter, one lowercase letter, and one number"
      ),

    confirmPassword: z.string("Please confirm your password"),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

export { registerFormSchema };

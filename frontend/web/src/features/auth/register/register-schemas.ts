import { z } from "zod";
import i18next from "@/utils/i18n";

const registerFormSchema = z
  .object({
    username: z
      .string(i18next.t("registerPage.error.usernameRequired"))
      .min(3, i18next.t("registerPage.error.usernameMinLength"))
      .regex(
        /^[A-Za-z0-9_]+$/,
        i18next.t("registerPage.error.usernameInvalid")
      ),

    email: z.preprocess(
      (value) => {
        if (typeof value !== "string") {
          return value;
        }
        const trimmedValue = value.trim();
        return trimmedValue === "" ? undefined : trimmedValue;
      },
      z.email(i18next.t("registerPage.error.emailInvalid")).optional()
    ),

    password: z
      .string(i18next.t("registerPage.error.passwordRequired"))
      .min(8, i18next.t("registerPage.error.passwordMinLength"))
      .regex(
        /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$/,
        i18next.t("registerPage.error.passwordInvalid")
      ),

    confirmPassword: z.string(
      i18next.t("registerPage.error.confirmPasswordRequired")
    ),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: i18next.t("registerPage.error.passwordsDoNotMatch"),
    path: ["confirmPassword"],
  });

export { registerFormSchema };

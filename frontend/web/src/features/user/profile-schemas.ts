import { z } from "zod";

import i18next from "@/utils/i18n";

const usernameSchema = z
  .string(i18next.t("registerPage.error.usernameRequired"))
  .min(3, i18next.t("registerPage.error.usernameMinLength"))
  .regex(/^[A-Za-z0-9_]+$/, i18next.t("registerPage.error.usernameInvalid"));

const optionalEmailSchema = z
  .string()
  .trim()
  .refine(
    (value) => value === "" || z.email().safeParse(value).success,
    i18next.t("registerPage.error.emailInvalid")
  );

const passwordSchema = z
  .string(i18next.t("registerPage.error.passwordRequired"))
  .min(8, i18next.t("registerPage.error.passwordMinLength"))
  .regex(
    /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$/,
    i18next.t("registerPage.error.passwordInvalid")
  );

const profileSettingsSchema = z.object({
  username: usernameSchema,
  email: optionalEmailSchema,
});

const changePasswordSchema = z
  .object({
    oldPassword: z
      .string(i18next.t("profilePage.password.error.currentPasswordRequired"))
      .min(1, i18next.t("profilePage.password.error.currentPasswordRequired")),
    newPassword: passwordSchema,
    confirmPassword: z.string(
      i18next.t("registerPage.error.confirmPasswordRequired")
    ),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: i18next.t("registerPage.error.passwordsDoNotMatch"),
    path: ["confirmPassword"],
  });

type ProfileSettingsFormFields = z.infer<typeof profileSettingsSchema>;
type ChangePasswordFormFields = z.infer<typeof changePasswordSchema>;

export {
  changePasswordSchema,
  profileSettingsSchema,
  type ChangePasswordFormFields,
  type ProfileSettingsFormFields,
};

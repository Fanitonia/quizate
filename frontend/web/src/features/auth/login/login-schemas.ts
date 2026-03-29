import { z } from "zod";
import i18next from "@/utils/i18n";

const loginFormSchema = z.object({
  usernameOrEmail: z
    .string()
    .min(1, i18next.t("loginPage.error.emailOrUsernameRequired")),
  password: z.string().min(1, i18next.t("loginPage.error.passwordRequired")),
});

export { loginFormSchema };

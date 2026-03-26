import { z } from "zod";
import { loginFormSchema } from "./login-schemas";

type LoginFormFields = z.infer<typeof loginFormSchema>;

export { type LoginFormFields };

import { z } from "zod";
import { registerFormSchema } from "./register-schemas";

type RegisterFormFields = z.infer<typeof registerFormSchema>;

export type { RegisterFormFields };

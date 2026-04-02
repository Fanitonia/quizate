import { useTranslation } from "react-i18next";

import CreateQuizForm from "./create-quiz-form";

function CreateQuizPage() {
  const { t } = useTranslation();

  return (
    <main className="mx-auto flex w-full max-w-7xl flex-1 flex-col gap-6 p-4 md:p-6">
      <header className="border-border/70 from-background via-muted/40 to-background rounded-2xl border bg-gradient-to-br p-6 shadow-sm">
        <div className="max-w-3xl space-y-2">
          <p className="text-primary text-sm font-medium tracking-[0.2em] uppercase">
            {t("createQuizPage.eyebrow")}
          </p>
          <h1 className="text-3xl font-semibold tracking-tight md:text-4xl">
            {t("createQuizPage.title")}
          </h1>
          <p className="text-muted-foreground text-sm leading-6 md:text-base">
            {t("createQuizPage.description")}
          </p>
        </div>
      </header>
      <CreateQuizForm />
    </main>
  );
}

export default CreateQuizPage;

import { Link } from "@tanstack/react-router";
import { useTranslation } from "react-i18next";
import { Separator } from "@/components/ui/separator";

function Footer() {
  const { t } = useTranslation();

  return (
    <footer className="w-full border-t">
      <div className="mx-auto flex max-w-5xl flex-col gap-4 px-6 py-6 sm:flex-row sm:items-center sm:justify-between">
        <Link to="/" className="text-foreground text-sm font-semibold">
          Quizate
        </Link>

        <nav className="flex gap-4 text-sm">
          <Link to="/" className="text-muted-foreground hover:text-foreground">
            {t("footer.home")}
          </Link>
        </nav>
      </div>

      <Separator />

      <div className="mx-auto max-w-5xl px-6 py-4">
        <p className="text-muted-foreground text-xs">
          {t("footer.copyright", { year: new Date().getFullYear() })}
        </p>
      </div>
    </footer>
  );
}

export default Footer;

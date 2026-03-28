import { useTranslation } from "react-i18next";

function Footer() {
  const { t } = useTranslation();

  return (
    <footer className="w-full border-t p-4 text-center text-sm">
      {t("footer.copyright", { year: new Date().getFullYear() })}
    </footer>
  );
}

export default Footer;

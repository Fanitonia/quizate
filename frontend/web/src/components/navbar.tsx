// EXTERNAL
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Link } from "@tanstack/react-router";
import { useState } from "react";
import { useTranslation } from "react-i18next";

// INTERNAL
import { logout } from "@/api/auth/auth-requests";
import {
  currentUserQueryKey,
  useCurrentUserQuery,
} from "@/api/auth/query-options";
import type { DetailedUserResponse } from "@/types/api/users";
import { useTheme } from "@/stores/theme-provider";
import { useUserStore } from "@/stores/user-store";

// COMPONENTS & ICONS
import {
  LogOut,
  Menu,
  Moon,
  Plus,
  Sun,
  User,
  Globe,
  Search,
  List,
} from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  Sheet,
  SheetContent,
  SheetFooter,
  SheetHeader,
  SheetTrigger,
} from "@/components/ui/sheet";

type NavbarUser = DetailedUserResponse | null | undefined;

interface NavbarActionsProps {
  user: NavbarUser;
  onLogout: () => void;
}

interface AuthButtonsProps {
  onNavigate?: () => void;
}

// TODO: search butonunu çalışır hale getir

function Navbar() {
  const { data: currentUser } = useCurrentUserQuery();
  const queryClient = useQueryClient();

  const { mutate: logoutMutate } = useMutation({
    mutationFn: logout,
    onSuccess: async () => {
      useUserStore.getState().logout();
      await queryClient.cancelQueries({ queryKey: currentUserQueryKey });
      queryClient.setQueryData(currentUserQueryKey, null);
    },
  });

  return (
    <nav className="bg-background/80 sticky top-0 grid w-full grid-cols-[1fr_auto_1fr] items-center gap-2 border-b px-4 py-3 md:flex md:flex-row md:justify-around md:gap-4">
      {/* Mobile search button */}
      <Button
        className="justify-self-start md:hidden"
        variant="ghost"
        size="icon"
      >
        <Search className="size-6" />
      </Button>
      {/* TODO: logo ile değiştir*/}
      <Link to="/" className="text-xl">
        Quizate
      </Link>
      {/* Responsive actions */}
      <div className="col-start-3 flex flex-row items-center justify-self-end">
        <DesktopActions user={currentUser} onLogout={logoutMutate} />
        <MobileActions user={currentUser} onLogout={logoutMutate} />
      </div>
    </nav>
  );
}

function DesktopActions({ user, onLogout }: NavbarActionsProps) {
  const { t } = useTranslation();

  return (
    <div className="hidden items-center justify-center gap-2 md:flex md:flex-row">
      <Button variant="ghost">{t("navbar.categories")}</Button>
      <Button className="justify-self-start" variant="ghost" size="icon">
        <Search className="size-5" />
      </Button>
      <Button variant="secondary">
        <Plus /> {t("navbar.create")}
      </Button>

      {user ? (
        <AvatarDropdown user={user} onLogout={onLogout} />
      ) : (
        <AuthButtons />
      )}
      <LanguageSelector />
      <ToggleThemeButton />
    </div>
  );
}

function MobileActions({ user, onLogout }: NavbarActionsProps) {
  const { t } = useTranslation();

  const [isOpen, setIsOpen] = useState(false);

  const closeMenu = () => setIsOpen(false);
  const handleLogout = () => {
    onLogout();
    closeMenu();
  };

  return (
    <div className="md:hidden">
      <Sheet open={isOpen} onOpenChange={setIsOpen}>
        <SheetTrigger
          aria-label="Open menu"
          className="flex items-center justify-center"
        >
          <Menu size={28} />
        </SheetTrigger>
        <SheetContent className="w-full! px-6 opacity-95">
          <SheetHeader>
            {/* TODO: logo ile değiştir*/}
            <h1 className="text-2xl">Quizate</h1>
          </SheetHeader>
          <div className="flex flex-row">
            <Button variant="ghost" size="xl">
              <List />
              <p className="text-lg">{t("navbar.categories")}</p>
            </Button>
          </div>
          <SheetFooter>
            <Button size="xl">
              <Plus /> <p className="text-base">{t("navbar.create")}</p>
            </Button>
            {user ? (
              <Button size="xl" variant="secondary">
                <UserAvatar user={user as DetailedUserResponse} />
                <p className="text-base">{t("profile")}</p>
              </Button>
            ) : (
              <MobileAuthButtons onNavigate={closeMenu} />
            )}
            <div className="flex items-center justify-center gap-1">
              <LanguageSelector xl={true} ghost={false} className="flex-2" />
              <ToggleThemeButton className="flex-1" xl={true} ghost={false} />
              {user && (
                <Button
                  variant="destructive"
                  size="xl"
                  className="text-destructive flex-2"
                  onClick={handleLogout}
                >
                  <LogOut /> {t("logout")}
                </Button>
              )}
            </div>
          </SheetFooter>
        </SheetContent>
      </Sheet>
    </div>
  );
}

function AuthButtons({ onNavigate }: AuthButtonsProps) {
  const { t } = useTranslation();

  return (
    <ButtonGroup className="flex w-full">
      <Button variant="secondary" className="flex-1" onClick={onNavigate}>
        <Link to="/login">{t("login")}</Link>
      </Button>
      <Button className="flex-1" onClick={onNavigate}>
        <Link to="/register">{t("signup")}</Link>
      </Button>
    </ButtonGroup>
  );
}

function MobileAuthButtons({ onNavigate }: AuthButtonsProps) {
  const { t } = useTranslation();

  return (
    <ButtonGroup className="flex w-full">
      <Button
        variant="secondary"
        size="xl"
        className="flex-1"
        onClick={onNavigate}
      >
        <Link to="/login">
          <p className="text-base">{t("login")}</p>
        </Link>
      </Button>
      <Button className="flex-1" size="xl" onClick={onNavigate}>
        <Link to="/register">
          <p className="text-base">{t("signup")}</p>
        </Link>
      </Button>
    </ButtonGroup>
  );
}

function UserAvatar({ user }: { user: DetailedUserResponse }) {
  const avatarFallback = user.username.slice(0, 2).toUpperCase();

  return (
    <Avatar>
      <AvatarImage src={user.profilePictureUrl ?? ""} />
      <AvatarFallback>{avatarFallback}</AvatarFallback>
    </Avatar>
  );
}

function AvatarDropdown({
  user,
  onLogout,
}: {
  user: DetailedUserResponse;
  onLogout: () => void;
}) {
  const { t } = useTranslation();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger aria-label="Open account menu">
        <UserAvatar user={user} />
      </DropdownMenuTrigger>
      <DropdownMenuContent sideOffset={20} className="min-w-fit px-3 py-2">
        <DropdownMenuGroup>
          <DropdownMenuLabel>{t("myAccount")}</DropdownMenuLabel>
          <DropdownMenuSeparator />
          <DropdownMenuItem>
            <User />
            {t("profile")}
          </DropdownMenuItem>
          <DropdownMenuItem variant="destructive" onClick={onLogout}>
            <LogOut />
            {t("logout")}
          </DropdownMenuItem>
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

function LanguageSelector({
  className,
  xl = false,
  ghost = true,
}: {
  className?: string;
  xl?: boolean;
  ghost?: boolean;
}) {
  const { i18n } = useTranslation();

  function setLanguage(lang: string) {
    i18n.changeLanguage(lang);
  }

  return (
    <DropdownMenu>
      <DropdownMenuTrigger
        render={
          <Button
            variant={ghost ? "ghost" : "secondary"}
            className={className}
            size={xl ? "xl" : "default"}
          >
            <Globe className="h-4 w-4" />
            {i18n.language.toUpperCase().slice(0, 2)}
          </Button>
        }
      ></DropdownMenuTrigger>
      <DropdownMenuContent sideOffset={8}>
        <DropdownMenuGroup>
          <DropdownMenuRadioGroup
            onValueChange={setLanguage}
            value={i18n.language}
          >
            <DropdownMenuRadioItem value="en">
              <span className="flex items-center gap-2">
                <span>EN</span>
                <span>English</span>
              </span>
            </DropdownMenuRadioItem>
            <DropdownMenuRadioItem value="tr">
              <span className="flex items-center gap-2">
                <span>TR</span>
                <span>Türkçe</span>
              </span>
            </DropdownMenuRadioItem>
          </DropdownMenuRadioGroup>
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

function ToggleThemeButton({
  className,
  ghost = true,
  xl = false,
}: {
  className?: string;
  ghost?: boolean;
  xl?: boolean;
}) {
  const { setTheme } = useTheme();

  const toggleTheme = () => {
    const currentTheme = localStorage.getItem("ui-theme");
    const nextTheme = currentTheme === "light" ? "dark" : "light";

    setTheme(nextTheme);
  };

  return (
    <Button
      variant={ghost ? "ghost" : "secondary"}
      size={xl ? "icon-xl" : "icon-lg"}
      className={`${className}`}
      onClick={toggleTheme}
    >
      <Sun className="size-5 scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90" />
      <Moon className="absolute size-5 scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0" />
      <span className="sr-only">Toggle theme</span>
    </Button>
  );
}

export default Navbar;

// EXTERNAL IMPORTS
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Link } from "@tanstack/react-router";
import { LogOut, Menu, Moon, Plus, Settings, Sun, User } from "lucide-react";
import { useState, type ReactNode } from "react";

// API & TYPES
import { logout } from "@/api/auth/auth-requests";
import {
  currentUserQueryKey,
  useCurrentUserQuery,
} from "@/api/auth/query-options";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import {
  DropdownMenu,
  DropdownMenuContent,
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
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import { useTheme } from "@/stores/theme-provider";
import { useUserStore } from "@/stores/user-store";
import type { DetailedUserResponse } from "@/types/api/users";
import { Separator } from "@base-ui/react";

type NavbarUser = DetailedUserResponse | null | undefined;

interface NavbarActionsProps {
  user: NavbarUser;
  onLogout: () => void;
}

interface AuthButtonsProps {
  onNavigate?: () => void;
}

interface MobileMenuSectionProps {
  title: string;
  children: ReactNode;
}

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
    <nav className="bg-background/80 sticky top-0 flex w-full flex-row items-center justify-between gap-4 border-b px-4 py-2 sm:justify-around">
      <Link to="/" className="text-xl">
        Quizate
      </Link>
      <div className="flex flex-row items-center justify-center">
        <DesktopActions user={currentUser} onLogout={logoutMutate} />
        <MobileActions user={currentUser} onLogout={logoutMutate} />
      </div>
    </nav>
  );
}

function DesktopActions({ user, onLogout }: NavbarActionsProps) {
  return (
    <div className="hidden flex-row items-center justify-center gap-2 sm:flex">
      <Button variant="secondary">
        <Plus /> New Quiz
      </Button>
      <Button variant="ghost">Categories</Button>
      <ToggleThemeButton />
      {user ? (
        <AvatarDropdown user={user} onLogout={onLogout} />
      ) : (
        <AuthButtons />
      )}
    </div>
  );
}

function MobileActions({ user, onLogout }: NavbarActionsProps) {
  const [isOpen, setIsOpen] = useState(false);

  const closeMenu = () => setIsOpen(false);
  const handleLogout = () => {
    onLogout();
    closeMenu();
  };

  return (
    <div className="flex flex-row gap-2 sm:hidden">
      <Button variant="secondary">
        <Plus /> Create Quiz
      </Button>
      <Sheet open={isOpen} onOpenChange={setIsOpen}>
        <SheetTrigger aria-label="Open menu">
          <Menu size={32} />
        </SheetTrigger>
        <SheetContent className="px-6">
          <SheetHeader>
            <SheetTitle>Menu</SheetTitle>
          </SheetHeader>
          {user ? (
            <MobileMenuSection title="My Account">
              <Button variant="ghost" className="w-full">
                <User />
                Profile
              </Button>
              <Button variant="ghost" className="w-full">
                <Settings />
                Settings
              </Button>
              <Button
                variant="ghost"
                className="text-destructive w-full"
                onClick={handleLogout}
              >
                <LogOut />
                Logout
              </Button>
            </MobileMenuSection>
          ) : (
            <AuthButtons onNavigate={closeMenu} />
          )}
          <MobileMenuSection title="Quizzes">
            <Button variant="ghost" className="w-full">
              Categories
            </Button>
          </MobileMenuSection>
          <SheetFooter>
            <ToggleThemeButton />
          </SheetFooter>
        </SheetContent>
      </Sheet>
    </div>
  );
}

function AuthButtons({ onNavigate }: AuthButtonsProps) {
  return (
    <ButtonGroup className="flex w-full">
      <Button
        variant="secondary"
        className="flex-1"
        render={<Link to="/login" />}
        onClick={onNavigate}
      >
        Login
      </Button>
      <Button
        className="flex-1"
        render={<Link to="/register" />}
        onClick={onNavigate}
      >
        Signup
      </Button>
    </ButtonGroup>
  );
}

function MobileMenuSection({ title, children }: MobileMenuSectionProps) {
  return (
    <div className="flex flex-col items-center justify-center gap-1">
      <h2 className="text-muted-foreground text-sm">{title}</h2>
      <Separator className="bg-border h-px w-full" />
      {children}
    </div>
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
  return (
    <DropdownMenu>
      <DropdownMenuTrigger aria-label="Open account menu">
        <UserAvatar user={user} />
      </DropdownMenuTrigger>
      <DropdownMenuContent sideOffset={16} className="min-w-fit p-2">
        <DropdownMenuGroup>
          <DropdownMenuLabel>My Account</DropdownMenuLabel>
          <DropdownMenuSeparator />
          <DropdownMenuItem>
            <User />
            Profile
          </DropdownMenuItem>
          <DropdownMenuItem>
            <Settings />
            Settings
          </DropdownMenuItem>
          <DropdownMenuItem variant="destructive" onClick={onLogout}>
            <LogOut />
            Logout
          </DropdownMenuItem>
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

function ToggleThemeButton() {
  const { setTheme } = useTheme();

  const toggleTheme = () => {
    const currentTheme = localStorage.getItem("ui-theme");
    const nextTheme = currentTheme === "light" ? "dark" : "light";

    setTheme(nextTheme);
  };

  return (
    <Button variant="ghost-full" size="icon-lg" onClick={toggleTheme}>
      <Sun className="size-[1.2rem] scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90" />
      <Moon className="absolute size-[1.2rem] scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0" />
      <span className="sr-only">Toggle theme</span>
    </Button>
  );
}

export default Navbar;

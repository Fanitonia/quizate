// EXTERNAL IMPORTS
import { Link } from "@tanstack/react-router";
import { useMutation } from "@tanstack/react-query";
import { useQueryClient } from "@tanstack/react-query";

// API & TYPES
import {
  currentUserQueryKey,
  useCurrentUserQuery,
} from "@/api/auth/query-options";
import type { DetailedUserResponse } from "@/types/api/users";
import { useTheme } from "@/stores/theme-provider";
import { useUserStore } from "@/stores/user-store";

// UI COMPONENTS
import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { LogOut, Moon, Plus, Settings, Sun, User } from "lucide-react";

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { logout } from "@/api/auth/auth-requests";

function Navbar() {
  // THEME TOGGLING
  const { setTheme } = useTheme();

  function toggleTheme() {
    var theme = localStorage.getItem("ui-theme");
    setTheme(theme === "light" ? "dark" : "light");
  }

  // AUTH
  const { data: currentUser } = useCurrentUserQuery();

  return (
    <nav className="bg-background/80 sticky top-0 flex w-full flex-row items-center justify-around gap-4 border-b px-4 py-2">
      {/* TODO: Replace with logo */}
      <Link to="/" className="text-xl">
        Quizate
      </Link>
      <div className="flex flex-row items-center justify-center gap-2">
        {/* Navigation buttons */}
        <Button variant="secondary">
          <Plus /> New Quiz
        </Button>
        <Button variant="ghost">Categories</Button>
        <Button variant="ghost-full" size="icon-lg" onClick={toggleTheme}>
          <Sun className="size-[1.2rem] scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90" />
          <Moon className="absolute size-[1.2rem] scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0" />
          <span className="sr-only">Toggle theme</span>
        </Button>
        {/* User avatar or login/signup buttons */}
        {currentUser ? (
          <UserAvatar user={currentUser} />
        ) : (
          <ButtonGroup>
            <Button variant="secondary">
              <Link to="/login">Login</Link>
            </Button>
            <Button>
              <Link to="/register">Signup</Link>
            </Button>
          </ButtonGroup>
        )}
      </div>
    </nav>
  );
}

function UserAvatar({ user }: { user: DetailedUserResponse }) {
  const queryClient = useQueryClient();

  const { mutate: logoutMutate } = useMutation({
    mutationFn: () => logout(),
    onSuccess: async () => {
      useUserStore.getState().logout();
      await queryClient.cancelQueries({ queryKey: currentUserQueryKey });
      queryClient.setQueryData(currentUserQueryKey, null);
    },
  });

  const avatarFallback = user.username.slice(0, 2).toUpperCase();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger>
        <Avatar>
          <AvatarImage src={user.profilePictureUrl ?? ""} />
          <AvatarFallback>{avatarFallback}</AvatarFallback>
        </Avatar>
      </DropdownMenuTrigger>
      <DropdownMenuContent sideOffset={20}>
        <DropdownMenuGroup>
          <DropdownMenuLabel>My Account</DropdownMenuLabel>
          <DropdownMenuSeparator></DropdownMenuSeparator>
          <DropdownMenuItem>
            <User />
            Profile
          </DropdownMenuItem>
          <DropdownMenuItem>
            <Settings />
            Settings
          </DropdownMenuItem>
          <DropdownMenuItem
            variant="destructive"
            onClick={() => logoutMutate()}
          >
            <LogOut />
            Logout
          </DropdownMenuItem>
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

export default Navbar;

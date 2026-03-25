import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import { Moon, Plus, Sun } from "lucide-react";
import { useTheme } from "@/stores/theme-provider";
import { Link } from "@tanstack/react-router";

function Navbar() {
  const { setTheme } = useTheme();

  function toggleTheme() {
    var theme = localStorage.getItem("ui-theme");
    setTheme(theme === "light" ? "dark" : "light");
  }

  return (
    <nav className="sticky top-0 flex w-full flex-row items-center justify-around gap-4 border-b px-4 py-2">
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
        <ButtonGroup>
          <Button variant="secondary">
            <Link to="/login">Login</Link>
          </Button>
          <Button>
            <Link to="/register">Signup</Link>
          </Button>
        </ButtonGroup>
        {/* User avatar and dropdown menu */}
        {/* <DropdownMenu>
          <DropdownMenuTrigger>
            <Avatar>
              <AvatarImage src="https://github.com/shadcn.png" />
              <AvatarFallback>CN</AvatarFallback>
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
              <DropdownMenuItem variant="destructive">
                <LogOut />
                Logout
              </DropdownMenuItem>
            </DropdownMenuGroup>
          </DropdownMenuContent>
        </DropdownMenu> */}
      </div>
    </nav>
  );
}

export default Navbar;

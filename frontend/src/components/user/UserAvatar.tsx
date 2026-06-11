import type { DetailedUserInfo, UserInfo } from "@type/api/users";

import { Avatar, AvatarFallback, AvatarImage } from "@components/ui/avatar";

interface UserAvatarProps {
  user: DetailedUserInfo | UserInfo;
  classNames?: string;
  size?: "sm" | "default" | "lg" | "xl";
}

function UserAvatar({ user, classNames, size }: UserAvatarProps) {
  const avatarFallback = user.username.slice(0, 1).toUpperCase();

  return (
    <Avatar className={classNames} size={size}>
      <AvatarImage src={user.profilePictureUrl ?? ""} />
      <AvatarFallback>{avatarFallback}</AvatarFallback>
    </Avatar>
  );
}

export { UserAvatar };

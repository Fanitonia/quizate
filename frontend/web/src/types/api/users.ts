interface UserInfo {
  id: string;
  createdAt: string;
  username: string;
  displayName: string;
  profilePictureUrl: string | null;
  role: string;
}

interface DetailedUserInfo extends UserInfo {
  email: string | null;
  isEmailVerified: boolean;
}

export type { UserInfo, DetailedUserInfo };

import type { UserInfo } from "@type/api/users";

import { ProfileOverviewCard } from "./shared/ProfileOverviewCard";
import { ProfileQuizzesSection } from "./shared/ProfileQuizzesSection";

function PublicUserProfile({ userData }: { userData: UserInfo }) {
  return (
    <div className="mx-auto flex w-full max-w-4xl flex-col gap-4 px-3 py-4 sm:px-4">
      <ProfileOverviewCard userData={userData} />
      <ProfileQuizzesSection username={userData.displayName} />
    </div>
  );
}

export { PublicUserProfile };

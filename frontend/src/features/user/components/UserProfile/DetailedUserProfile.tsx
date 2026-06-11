import type { DetailedUserInfo } from "@type/api/users";

import { ProfileOverviewCard } from "./shared/ProfileOverviewCard";
import { ProfileQuizzesSection } from "./shared/ProfileQuizzesSection";

function DetailedUserProfile({ userData }: { userData: DetailedUserInfo }) {
  return (
    <div className="mx-auto flex w-full max-w-4xl flex-col gap-6 px-3 py-4 sm:px-4">
      <ProfileOverviewCard showActions userData={userData} />
      <ProfileQuizzesSection username={userData.displayName} />
    </div>
  );
}

export { DetailedUserProfile };

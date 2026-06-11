/* eslint-disable react-refresh/only-export-components */
import { createFileRoute } from "@tanstack/react-router";
import { AxiosError } from "axios";
import { useTranslation } from "react-i18next";

import {
  DetailedUserProfile,
  PublicUserProfile,
} from "@/features/user/components/UserProfile";

import {
  fetchCurrentUser,
  useCurrentUserQuery,
} from "@api/current-user/getCurrentUser";
import {
  getUserByUsernameQueryOptions,
  useGetUserByUsernameQuery,
} from "@api/user";

import { UserRoundX } from "lucide-react";

import {
  ComponentLoader,
  NotFound,
  SomethingGoneWrong,
} from "@components/feedback";

export const Route = createFileRoute("/profile/$username")({
  component: RouteComponent,
  loader: async ({ params, context }) => {
    const currentUser = await fetchCurrentUser(context.queryClient);
    const { username } = params;

    function isOwnProfile() {
      return currentUser && username == currentUser.username;
    }

    if (isOwnProfile()) {
      return { isOwnProfile: true };
    } else {
      await context.queryClient.fetchQuery(
        getUserByUsernameQueryOptions(username)
      );
      return { isOwnProfile: false };
    }
  },
  pendingMs: 100,
  pendingComponent: ComponentLoader,
  errorComponent: ({ error }) => <ErrorComponent error={error} />,
});

function RouteComponent() {
  const { isOwnProfile } = Route.useLoaderData();

  const { data: user } = useGetUserByUsernameQuery(
    Route.useParams().username,
    !isOwnProfile
  );
  const { data: currentUser } = useCurrentUserQuery();

  if (isOwnProfile && currentUser) {
    return <DetailedUserProfile userData={currentUser} />;
  } else if (user) {
    return <PublicUserProfile userData={user} />;
  } else {
    return <SomethingGoneWrong />;
  }
}

function ErrorComponent({ error }: { error: Error }) {
  const { t } = useTranslation();

  if (error instanceof AxiosError && error.response?.status === 404) {
    return (
      <NotFound
        title={t("notFound.user.title")}
        description={t("notFound.user.description")}
        icon={<UserRoundX className="text-destructive size-8" />}
      />
    );
  }

  return <SomethingGoneWrong />;
}

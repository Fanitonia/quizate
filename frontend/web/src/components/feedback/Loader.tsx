import { Spinner } from "../ui/spinner";

function ComponentLoader() {
  return (
    <div className="flex flex-1 items-center justify-center p-4">
      <Spinner className="size-10" />
    </div>
  );
}

export { ComponentLoader };

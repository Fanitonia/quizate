import { Spinner } from "../ui/spinner";

function ComponentLoader({ spinnerClassName }: { spinnerClassName?: string }) {
  return (
    <div className="flex flex-1 items-center justify-center">
      <Spinner className={`size-10 ${spinnerClassName}`} />
    </div>
  );
}

export { ComponentLoader };

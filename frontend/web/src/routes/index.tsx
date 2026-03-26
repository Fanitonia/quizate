// EXTERNAL LIBRARIES
import { createFileRoute } from "@tanstack/react-router";

// COMPONENTS
import { Construction } from "lucide-react";

export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  return (
    <div className="flex flex-1 flex-col items-center justify-center gap-2 p-4">
      <Construction size="48" />
      <h3 className="text-foreground text-2xl">Work in Progress</h3>
    </div>
  );
}

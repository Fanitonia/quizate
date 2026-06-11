const userQueryKeys = {
  info: (username: string) => ["user", username] as const,
};

export { userQueryKeys };

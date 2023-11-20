namespace RapidPayService.Helpers
{
    public static class ObjectHelpers
    {
        public static bool IsAnyNull(params object[] args)
        {

            return args.Any(x => x == null);
        }

        public static bool IsAllNull(params object[] args)
        {
            return args.All(x => x == null);
        }
    }
}

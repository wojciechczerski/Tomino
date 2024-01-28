namespace Tomino.Shared
{
    public static class BoolExtension
    {
        public static int IntValue(this bool value)
        {
            return value ? 1 : 0;
        }
    }
}

namespace Utils.Security
{
    public static class Key
    {
        private static Random random = new Random();
        public static string? Genera()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#$%&?¿¡!+_-.,;><@";
            return new string(Enumerable.Repeat(characters, 200).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

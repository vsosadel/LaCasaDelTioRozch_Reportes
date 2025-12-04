using System.Text;

namespace Utils.helpers
{
    public static class ExceptionHelper
    {
        public static string GetFullExceptionDetails(Exception ex)
        {
            if (ex == null) return string.Empty;

            var sb = new StringBuilder();
            int level = 0;

            while (ex != null)
            {
                sb.AppendLine($"--- Exception Nivel {level} ---");
                sb.AppendLine($"Tipo: {ex.GetType().FullName}");
                sb.AppendLine($"Mensaje: {ex.Message}");
                sb.AppendLine($"StackTrace: {ex.StackTrace}");
                sb.AppendLine();

                ex = ex.InnerException;
                level++;
            }

            return sb.ToString();
        }
    }
}

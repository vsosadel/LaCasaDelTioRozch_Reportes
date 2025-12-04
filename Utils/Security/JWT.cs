using Microsoft.AspNetCore.Http;
using System.Runtime.Caching;
using System.Security.Claims;

namespace Utils.Security
{
    public static class JWT
    {
        private static IHttpContextAccessor? _hAccessor;

        public static void Inicializar(IHttpContextAccessor? hAccessor)
        {
            _hAccessor = hAccessor;
        }

        public static string ObtieneEmail()
        {
            return _hAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
        public static List<Claim> ObtieneClaims()
        {          
            return _hAccessor?.HttpContext?.User.Claims.ToList() ?? new List<Claim>();
        }
        public static string Obtener()
        {
            string token = string.Empty;
            if (_hAccessor != null && _hAccessor.HttpContext != null)
            {
                string jwt = _hAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                token = !string.IsNullOrEmpty(jwt) ? jwt.Replace("Bearer ", string.Empty).Replace("bearer ", string.Empty) : string.Empty;
            }
            return token;
        }
        public static void Inhabilita()
        {
            string jwt = Obtener();
            if (!string.IsNullOrEmpty(jwt))
            {
                MemoryCache oCache = MemoryCache.Default;
                var lTokens = new List<string>();
                if (oCache["JWTExpirados"] != null)
                {
                    lTokens = (List<string>)oCache["JWTExpirados"];
                    lTokens.Add(jwt);
                    oCache["JWTExpirados"] = lTokens;
                }
                else
                {
                    lTokens.Add(jwt);
                    oCache["JWTExpirados"] = lTokens;
                }
            }
        }
        public static bool Valida()
        {
            MemoryCache oCache = MemoryCache.Default;
            var lTokens = new List<string>();
            if (oCache["JWTExpirados"] != null)
            {
                lTokens = (List<string>)oCache["JWTExpirados"];
            }
            return lTokens.Where(x => x == Obtener()).FirstOrDefault() != null;
        }
        public static bool Expirado()
        {
            Claim? expiracion = _hAccessor != null && _hAccessor.HttpContext != null ? _hAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "exp") : null;
            bool bActivo = true;
            if (expiracion != null && !string.IsNullOrEmpty(expiracion.Value))
            {
                DateTime fechaExpiracion = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiracion.Value)).DateTime;
                if (DateTime.UtcNow <= fechaExpiracion)
                {
                    bActivo = false;
                }
            }
            return bActivo;
        }
    }
}

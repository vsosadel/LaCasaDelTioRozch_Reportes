using System.Text;
using System.Text.Json;
using Utils.Response;
using Utils.Security;

namespace Utils.Peticiones
{
    public static class Http<T>
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<Respuesta<T>> ConsumirApiAsync(string url, HttpMethod metodo, object? request, Dictionary<string, string>? headers = null)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(metodo, url);

                // Si trae body (POST, PUT, etc.)
                if (request != null && (metodo == HttpMethod.Post || metodo == HttpMethod.Put || metodo.Method == "PATCH"))
                {
                    var json = JsonSerializer.Serialize(request);
                    requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                // Headers opcionales
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                var authHeader = requestMessage.Headers.Authorization;
                if (authHeader == null || string.IsNullOrEmpty(authHeader.Scheme))
                {
                    requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT.Obtener() ?? string.Empty);
                }

                using var response = await client.SendAsync(requestMessage);

                var contenido = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta<T>
                    {
                        exitoso = false,
                        mensaje = $"Error: {response.StatusCode}, {contenido}"
                    };
                }
                Respuesta<T>? data;
                try
                {
                    // Deserializar respuesta
                    data = JsonSerializer.Deserialize<Respuesta<T>>(contenido, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                }
                catch (Exception ex)
                {
                    data = new Respuesta<T>
                    {
                        exitoso = false,
                        mensaje = "Error al deserializar la respuesta. --> " + ex.Message
                    };
                }

                return data ?? new Respuesta<T>
                {
                    exitoso = false,
                    mensaje = "Error al deserializar la respuesta."
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<T>
                {
                    exitoso = false,
                    mensaje = $"Excepción: {ex.Message}"
                };
            }
        }
    }
}

using Newtonsoft.Json;

namespace Utils.Converters
{
    public class DateTimeUnspecifiedConverter : JsonConverter<DateTime>
    {
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            // Se escribe igual
            writer.WriteValue(value);
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return default;

            // Convierte a DateTime
            var dt = Convert.ToDateTime(reader.Value);

            // Cambia el Kind a Unspecified
            return DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
        }
    }
}

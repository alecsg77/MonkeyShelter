using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Data
{
    public static class MonkeyCollection
    {
        public class DateTimeCustomConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
            }
        }

        static MonkeyCollection()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeCustomConverter());
            using var stream = assembly.GetManifestResourceStream("MonkeyShelter.Data.monkeycollection.json");
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException("Missing monkeycollection.json resource"));
            Monkeys = JsonSerializer.Deserialize<Monkey[]>(reader.ReadToEnd(), options);
        }
        public static readonly Monkey[] Monkeys;
    }
}

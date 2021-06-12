using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestaurantReviews.Web
{
    internal class AddressJsonConverter : JsonConverter<IAddress>
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(IAddress);

        public override IAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                string jsonText = jsonDoc.RootElement.GetRawText();
                return JsonSerializer.Deserialize<Address>(jsonText, options);
            }
        }

        public override void Write(Utf8JsonWriter writer, IAddress value, JsonSerializerOptions options)
        {
            var concreteValue = new Address(value);
            JsonSerializer.Serialize(writer, concreteValue, typeof(Address), options);
        }
    }
}

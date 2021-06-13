using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestaurantReviews.Web
{
    /// <summary>
    /// Converts an interface type to a concrete type for JSon serialization
    /// </summary>
    /// <typeparam name="InterfaceType">Base interface</typeparam>
    /// <typeparam name="ConcreteType">Concrete type</typeparam>
    internal class InterfaceToConcreteJsonConverter<InterfaceType, ConcreteType> : JsonConverter<InterfaceType> where ConcreteType : InterfaceType
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(InterfaceType);

        public override InterfaceType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                string jsonText = jsonDoc.RootElement.GetRawText();
                return JsonSerializer.Deserialize<ConcreteType>(jsonText, options);
            }
        }

        public override void Write(Utf8JsonWriter writer, InterfaceType value, JsonSerializerOptions options)
        {
            //Convert the interface type to the concrete implementation, then serialize it
            var concreteValue = (ConcreteType) Activator.CreateInstance(typeof(ConcreteType), value);
            JsonSerializer.Serialize(writer, concreteValue, options);
        }
    }
}

using Newtonsoft.Json;
using System;

namespace ResurrectedEternal.Configs
{
    public class CustomWrapConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //does this throw? HUH
            //return base.CanConvert(objectType);
            //Thus it will return as int?
            return objectType == typeof(object);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.Value != null && reader.Value is SharpDX.Color)
            {
                return reader.Value;
            }

            if (reader.Value != null && reader.Value is long)
            {
                return Convert.ToInt32(reader.Value);
            }
            else if (reader.Value != null && reader.Value is double)
            {
                return Convert.ToSingle(reader.Value);
            }
            return reader.Value;
        }
    }
}

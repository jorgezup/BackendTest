using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BooksApi.Models
{
    public class Book
    {
        public int id { get; set; }
        
        // [FromQuery(Name="name")]
        public string name { get; set; }

        public decimal price { get; set; }

        public Specifications Specifications { get; set; }

        public decimal GetFrete(decimal price)
        {
            return price+(price*20/100);
        }
    }

    [JsonObject]
    public class Specifications
    {
        [JsonProperty(PropertyName = "Originally Published")]
        public string OriginallyPublished { get; set; }

        public string author { get; set; }

        [JsonProperty(PropertyName = "Page Count")]
        public int PageCount { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> illustrator { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> Genres { get; set; }
    }
    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objecType)
        {
            return (objecType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objecType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
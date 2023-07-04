using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DDDMicroservice.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, string> ToDictionary(this object obj)
        {
            return obj
                    .GetType()
                    .GetProperties()
                    .ToDictionary(p => SetPropertyName(p), p => p.GetValue(obj).ToString());
        }

        public static string SetPropertyName(PropertyInfo propertyInfo)
        {
            //check if a  [JsonProperty(PropertyName = "")] has been applied   
            var jsonProperty = propertyInfo.GetCustomAttributes<JsonPropertyAttribute>().FirstOrDefault();
            if (jsonProperty != null)
            {
                return jsonProperty.PropertyName.Trim('\"');
            }
            else
            {
                return propertyInfo.Name;
            }
        }
    }
}
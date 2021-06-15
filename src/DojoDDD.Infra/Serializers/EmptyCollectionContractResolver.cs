using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DojoDDD.Infra.Serializers
{
    public class EmptyCollectionContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            base.CreateProperty(member, memberSerialization);

            var property = base.CreateProperty(member, memberSerialization);

            var shouldSerialize = property.ShouldSerialize;
            property.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !IsEmptyCollection(property, obj);
            return property;
        }

        private static bool IsEmptyCollection(JsonProperty property, object target)
        {
            var value = property.ValueProvider?.GetValue(target);
            if (value is ICollection { Count: 0 }) return true;

            if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType)) return false;

            var countProp = property.PropertyType?.GetProperty("Count");

            var count = (int?) countProp?.GetValue(value, null);
            return count == 0;
        }
    }
}
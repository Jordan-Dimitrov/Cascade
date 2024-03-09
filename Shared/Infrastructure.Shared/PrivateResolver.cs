using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    public class PrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member,
            MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(
                member,
                memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;

                bool hasPrivateSetter = property?.GetSetMethod(true) != null;

                prop.Writable = hasPrivateSetter;
            }

            return prop;
        }
    }
}

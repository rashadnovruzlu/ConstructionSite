using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Extensions.Sessions
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            var result = session.GetString(key);
            return result == null ? default(T) : JsonConvert.DeserializeObject<T>(result);
        }
    }
}

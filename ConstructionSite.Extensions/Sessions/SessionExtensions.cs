using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ConstructionSite.Extensions.Sessions
{
    public static class SessionExtensions
    {
        #region .::SETSESSION::

        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        #endregion .::SETSESSION::

        #region .::GETSESSION::

        public static T GetSession<T>(this ISession session, string key)
        {
            var result = session.GetString(key);
            return result == null ? default(T) : JsonConvert.DeserializeObject<T>(result);
        }

        #endregion .::GETSESSION::
    }
}
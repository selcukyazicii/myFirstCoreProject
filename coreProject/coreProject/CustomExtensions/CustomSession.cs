using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace coreProject.CustomExtensions
{
    public static class CustomSession
    {
        public static void SetObject<T>(this ISession session, string key, T value) where T : class, new()
        {
            var data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }
        public static T GetObject<T>(this ISession session, string key) where T:class,new()
        {           
            var data = session.GetString(key);
            if (!string.IsNullOrWhiteSpace(data))
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            return null;
        }
    }
}

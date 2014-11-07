using System;
using AnthillaASCore.Models;

namespace AnthillaASCore
{
    public static class MapUserFromDb
    {
        public static Tuple<string, string, Guid> Map(Anth_Dump user)
        {
            string username = user.AnthillaAlias;
            string password = user.AnthillaPassword;
            Guid guid = Guid.Parse(user.AnthillaGuid);
            Tuple<string, string, Guid> tuple = new Tuple<string, string, Guid>(username, password, guid);
            return tuple;
        }
    }
}
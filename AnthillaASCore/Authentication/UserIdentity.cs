using System.Collections.Generic;
using Nancy.Security;

namespace AnthillaASCore
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}
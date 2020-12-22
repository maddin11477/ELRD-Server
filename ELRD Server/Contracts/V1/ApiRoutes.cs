using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Version = "v1";
        public const string Root = "api";

        public const string Base = Root + "/" + Version;

        public static class Users
        {
            public const string Login = Base + "/login";

            public const string GetAll =Base + "/users";

            public const string Get = Base + "/users/{userID}";

            public const string Update = Base + "/users/{userID}";

            public const string Delete = Base + "/users/{userID}";

            public const string Create = Base + "/users";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";
        }

        public static class BaseData
        {
            public const string BaseUnit = Base + "/basedata/baseunits";

            public const string SeedBaseUnit = Base + "/basedata/seedbaseunit";

        }
    }
}

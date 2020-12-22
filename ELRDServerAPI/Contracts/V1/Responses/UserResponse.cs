using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Contracts.V1.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

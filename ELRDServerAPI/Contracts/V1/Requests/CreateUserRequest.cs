using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Contracts.V1.Requests
{
    public class CreateUserRequest
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            string s = Environment.NewLine + "Userinformation: " + Environment.NewLine;
            s = s + "Firstname: " + Firstname + Environment.NewLine;
            s = s + "Lastname: " + Lastname + Environment.NewLine;
            s = s + "Username: " + Username;

            return s;
        }
    }
}

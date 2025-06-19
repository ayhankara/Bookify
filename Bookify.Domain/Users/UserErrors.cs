using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error NotFound = new(
            "User.Found",
            "The user with the specified identifier was not found");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials were invalid");
    }

}

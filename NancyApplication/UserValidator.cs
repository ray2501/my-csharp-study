using System.Security.Claims;
using System.Security.Principal;
using Nancy.Authentication.Basic;

namespace NancyApplication
{
    public class UserValidator : IUserValidator
    {
        public ClaimsPrincipal Validate(string username, string password)
        {
            if (username == "danilo" && password == "danilo")
            {
                return new ClaimsPrincipal(new GenericIdentity(username));
            }

            // Not recognised => anonymous.
            return null;
        }
    }
}

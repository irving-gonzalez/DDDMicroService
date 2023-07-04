using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace DDDMicroservice.Application.Authorization
{
    public static class BasicAuthDefaults
    {
        public static readonly string BasicAuthScheme = "Basic";
    }

    public class BasicAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public IEnumerable<BasicAuthUser> Users { get; set; } = new List<BasicAuthUser>();
    }
}
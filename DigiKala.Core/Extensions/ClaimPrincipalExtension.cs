using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static string AvatarName(this ClaimsPrincipal user)
        => user.FindFirst("AvatarName")!.Value;
    }
}

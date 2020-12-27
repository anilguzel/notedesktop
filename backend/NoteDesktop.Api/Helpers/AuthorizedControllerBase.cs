using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NoteDesktop.Api.Helpers
{
    public class AuthorizedControllerBase : ControllerBase
    {
        private string _userId;

        private string _email;

        public string UserId => _userId == null ? (_userId = GetUserId()) : null;

        public string Email => _email ?? (_email = GetEmail());

        [NonAction]
        public string GetEmail()
        {
            return GetClaimValue(ClaimTypes.Email);
        }

        [NonAction]
        public string GetUserId()
        {
            return GetClaimValue(ClaimTypes.NameIdentifier);
        }

        private string GetClaimValue(string type)
        {
            var user = HttpContext.User.Identities.FirstOrDefault();

            var claim = user?.Claims.FirstOrDefault(x => x.Type == type);

            return claim?.Value;
        }
    }
}
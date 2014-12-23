using model;
using nmct.ba.cashlessproject.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize]
    public class PasswordController : ApiController
    {
        public HttpResponseMessage Put(Password pw)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            PasswordDA.ChangePassword(pw, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

﻿using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;
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
    public class RegisterController : ApiController
    {
        public List<RegisterKassa> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            return RegisterDA.GetRegisters(p.Claims);
        }

        
    }
}

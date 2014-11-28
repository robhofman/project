using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize]
    public class CustomerController: ApiController
    {
        //ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
        public List<Customer> Get()
        {
            return CustomerDA.GetCustomers();
        }
    }
}
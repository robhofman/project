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
    public class SalesController : ApiController
    {
        public List<Sale> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            return SaleDA.GetSales(p.Claims);
        }

        public List<Sale> Get(int from, int until, int registerID, int productID)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            return SaleDA.GetSalesByCriteria(from, until, registerID, productID, p.Claims);
        }

        public HttpResponseMessage Post(Sale s)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            SaleDA.InsertSale(s, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

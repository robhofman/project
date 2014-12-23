using nmct.ba.cashlessproject.api.Models;
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
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            return ProductDA.GetProducts(p.Claims);
        }

        public Product Get(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            return ProductDA.GetProduct(id, p.Claims);
        }

        public HttpResponseMessage Post(Product prod)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            ProductDA.InsertProduct(prod, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(Product prod)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            ProductDA.EditProduct(prod, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal; 
            ProductDA.DeleteProduct(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
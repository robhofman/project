﻿using Microsoft.Owin.Security.OAuth;
using nmct.ba.cashlessproject.api.Controllers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ba.cashlessproject.api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Organisation o = OrganisationDA.CheckCredentials(context.UserName, context.Password);
            if (o == null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("dbname", o.DbName));
            id.AddClaim(new Claim("dblogin", o.Dblogin));
            id.AddClaim(new Claim("dbpass", o.Dbpassword));

            context.Validated(id);
            return Task.FromResult(0);
        }
    }
}
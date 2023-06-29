using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace FirstRazorApp
{

    public class EvenConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {

            int id;


            if (Int32.TryParse(values["id"].ToString(), out id))
            {

                if (id % 2 == 0)

                    return true;
            }

            return false;
        }
    }
}

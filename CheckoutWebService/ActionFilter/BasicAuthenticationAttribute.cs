using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Configuration;
using System.Web.Http.Controllers;
using System.Text;
using System.Net.Http;
using System.Net;

namespace CheckoutWebService.ActionFilter
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthenticationAttribute(string username, string password)
        {
            Username = ConfigurationManager.AppSettings[username];
            Password = ConfigurationManager.AppSettings[password];
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password))
            {
                return;
            }

            try
            {
                var auth = actionContext.Request.Headers.Authorization;
                if (auth != null)
                {
                    var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(auth.Parameter)).Split(':');
                    if (credentials[0] == Username &&
                        credentials[1] == Password)
                    {
                        return;
                    }
                }

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
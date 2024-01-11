using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using Zamowienia.Models;

namespace Zamowienia.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _requiredRole;

        public CustomAuthorizeAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
            var user = await userManager.GetUserAsync(context.HttpContext.User);

            if (user != null && user.TypUzytkownika == _requiredRole)
            {
                return;
            }
            context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }
    }
}
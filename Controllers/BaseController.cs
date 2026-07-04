using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingMVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = HttpContext.Session.GetInt32("IdKorisnik");

            if (id == null)
            {
                context.Result = RedirectToAction("Login", "Auth");
            }

            base.OnActionExecuting(context);
        }
    }
}

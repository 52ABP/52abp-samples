using Microsoft.AspNetCore.Antiforgery;
using postgresqlDemo.Controllers;

namespace postgresqlDemo.Web.Host.Controllers
{
    public class AntiForgeryController : postgresqlDemoControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

using Microsoft.AspNetCore.Antiforgery;
using ExcelImportDemo.Controllers;

namespace ExcelImportDemo.Web.Host.Controllers
{
    public class AntiForgeryController : ExcelImportDemoControllerBase
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

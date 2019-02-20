using Microsoft.AspNetCore.Antiforgery;
using MysqlMigrationDemo.Controllers;

namespace MysqlMigrationDemo.Web.Host.Controllers
{
    public class AntiForgeryController : MysqlMigrationDemoControllerBase
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

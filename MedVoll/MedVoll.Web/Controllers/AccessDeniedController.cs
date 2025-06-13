using Microsoft.AspNetCore.Mvc;

namespace MedVoll.Web.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult AccessDenied()
        {
            ViewBag.Message = "Você não tem permissão para acessar esta página verifique suas permissões.";
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJ_RYAN.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult Index()
        {
            //var usuarioLogado = HttpContext.Session["usuario"];
            //if(usuarioLogado == null)
            //{
            //    return Redirect("/LoginCadastro/Index");
            //}

            return View();
        }
    }
}
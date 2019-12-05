using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PROJ_RYAN.Models;

namespace PROJ_RYAN.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } 
        
        public ActionResult Upload(HttpPostedFileWrapper file1, HttpPostedFileWrapper file2, HttpPostedFileWrapper file3)
        {
            if (file1 != null && file2 != null && file3 != null)
            {
                file1.SaveAs(Server.MapPath("~/Repository/" + "Imagem1.png"));
                file2.SaveAs(Server.MapPath("~/Repository/" + "Imagem2.png"));
                file3.SaveAs(Server.MapPath("~/Repository/" + "Imagem3.png"));
                return Redirect("/Adm/Index");
            }
            else if (file1 != null && file2 != null && file3 == null)
            {
                file1.SaveAs(Server.MapPath("~/Repository/" + "Imagem1.png"));
                file2.SaveAs(Server.MapPath("~/Repository/" + "Imagem2.png"));
                return Redirect("/Adm/Index");
            }
            else if (file2 != null && file3 != null && file1 == null)
            {
                file2.SaveAs(Server.MapPath("~/Repository/" + "Imagem2.png"));
                file3.SaveAs(Server.MapPath("~/Repository/" + "Imagem3.png"));
                return Redirect("/Adm/Index");
            }
            else if (file1 != null && file3 != null && file2 == null)
            {
                file1.SaveAs(Server.MapPath("~/Repository/" + "Imagem1.png"));
                file3.SaveAs(Server.MapPath("~/Repository/" + "Imagem3.png"));
                return Redirect("/Adm/Index");
            }
            else if (file1 != null && file2 == null && file3 == null)
            {
                file1.SaveAs(Server.MapPath("~/Repository/" + "Imagem1.png"));
                return Redirect("/Adm/Index");
            }
            else if (file2 != null && file1 == null && file3 == null)
            {
                file2.SaveAs(Server.MapPath("~/Repository/" + "Imagem2.png"));
                return Redirect("/Adm/Index");
            }
            else if (file3 != null && file1 == null && file2 == null)
            {
                file3.SaveAs(Server.MapPath("~/Repository/" + "Imagem3.png"));
                return Redirect("/Adm/Index");
            }
            else
            {
                return Redirect("/Adm/Index");
            }
        }
    }
}
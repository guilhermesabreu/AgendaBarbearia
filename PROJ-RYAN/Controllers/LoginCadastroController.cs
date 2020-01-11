using PROJ_RYAN.Models;
using PROJ_RYAN.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROJ_RYAN.Controllers
{
    public class LoginCadastroController : Controller
    {
        MvcCRUDDBEntities1 ctx = new MvcCRUDDBEntities1();

        // GET: LoginCadastro
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(login usuario, string submitButton)
        {
            var senhaBytes = System.Text.Encoding.UTF8.GetBytes(usuario.senha);
            var senhaCriptografada = Convert.ToBase64String(senhaBytes);

            var consultaUsuario = ctx.logins.Where(u => u.login_email == usuario.login_email && u.senha == senhaCriptografada).FirstOrDefault();

            if (consultaUsuario != null)
            {
                switch (submitButton)
                {
                    case "Sign In":
                        Session.Add("usuario", consultaUsuario);
                        return Redirect("/AgendaAdm/ListaAgendadosAdm");
                    case "Update User":
                        return RedirectToAction("List");
                    default:
                        return View("Index");
                }
            }
            else
            {
                ViewBag.ErroAutenticacao = "Usuário ou senha inválidos.";
                return View("Index");
            }

        }
        [HttpGet]
        public ActionResult List()
        {
            using (ctx)
            {
                //Variável que representa as propriedades da classe de contexto(diretamente da entidade),chamada: logins.
                var propriedadesDaEntidade = ctx.logins.ToList();
                //Instanciando a variável que representa as propriedades da classe loginViewModel em uma List List.
                var propriedadesDaModelVM = new List<loginViewModel>();

                foreach (var item in propriedadesDaEntidade)
                {
                    //Populando as propriedades de loginViewModel com as propriedades da entidade
                    var x = new loginViewModel
                    {
                        id_login = item.id_login,
                        nome = item.nome,
                        senha = item.senha,
                        login_email = item.login_email,
                        data_cadastro = item.data_cadastro
                    };
                    // Adcionando os novos valores populados na variável que representa as prop. da VM
                    propriedadesDaModelVM.Add(x);
                }

                //Retornando as propriedades da VM já populadas com a da Entidade
                return View(propriedadesDaModelVM);
            }  
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (ctx)
            {
                return View(ctx.logins.Where(x => x.id_login == id).FirstOrDefault());
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (ctx)
            {
                return View(ctx.logins.Where(x => x.id_login == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, login usuario)
        {
            //var senhaCrip = FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.senha, "MD5");
            using (ctx)
            {
                var login = (from l in ctx.logins
                             where l.id_login == usuario.id_login
                             select l).First();

                if (login != null)
                {
                    var senhaBytes = System.Text.Encoding.UTF8.GetBytes(usuario.senha);
                    var senhaCriptografada = Convert.ToBase64String(senhaBytes);

                    login.senha = senhaCriptografada;

                    login.nome = usuario.nome;
                    login.login_email = usuario.login_email;
                }

                ctx.SaveChanges();
            }

            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ctx)
            {
                return View(ctx.logins.Where(x => x.id_login == id).First());
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, login usuario)
        {
            try
            {
                using (ctx)
                {
                    usuario = ctx.logins.Where(x => x.id_login == id).First();
                    ctx.logins.Remove(usuario);
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
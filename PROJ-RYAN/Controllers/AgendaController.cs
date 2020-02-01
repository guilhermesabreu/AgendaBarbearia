using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PROJ_RYAN.Models;
using PROJ_RYAN.ViewModels;

namespace PROJ_RYAN.Controllers
{
    public class AgendaController : Controller
    {
        private barbeariaEntities ctx = new barbeariaEntities();

        // GET: Agenda2
        [HttpGet]
        public async Task<ActionResult> ListaAgendadosCliente(bool partial = false)
        {
            using (ctx)
            {
                //Variável que representa as propriedades da classe de contexto(diretamente da entidade),chamada: logins.
                var agendamentos = ctx.Agenda.ToList();
                //Instanciando a variável que representa as propriedades da classe loginViewModel em uma List List.
                var propriedadesDaModelVM = new List<AgendaViewModel>();

                DeletarAgendamentoAutomaticamente(agendamentos);

                var agendamentoAtualizado = await ctx.Agenda.ToListAsync();
                foreach (var agendamento in agendamentoAtualizado)
                {
                    //Populando as propriedades de loginViewModel com as propriedades da entidade
                    var x = new AgendaViewModel
                    {
                        ClienteId = agendamento.ClienteId,
                        Nome = agendamento.Nome,
                        Email = agendamento.Email,
                        Data = agendamento.DataHora,
                        Hora = agendamento.Hora,
                        Celular = agendamento.Celular
                    };
                    // Adcionando os novos valores populados na variável que representa as prop. da VM
                    propriedadesDaModelVM.Add(x);
                }

                var diasAgendadosComDistinctOrdenado = ctx.Agenda.OrderBy(x => x.DataHora).ToList().Select(x => x.DataHora.Day + "/" + x.DataHora.Month + "/" + x.DataHora.Year).Distinct();

                ViewBag.DiaComDistinctOrdenado = diasAgendadosComDistinctOrdenado;

                //Retornando as propriedades da VM já populadas com a da Entidade
                if (partial)
                    return PartialView(propriedadesDaModelVM);
                else
                    return View(propriedadesDaModelVM);
            }
        }
  
        public async void DeletarAgendamentoAutomaticamente(List<Agenda> agendas)
        {
            var horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            TimeSpan duracaoServico = new TimeSpan(0, 20, 0);
            var idDataHoraServicoFinalizado = agendas.Where(a => a.DataHora <= DateTime.Now.Date && a.Hora.Value.Add(duracaoServico) <= horaAtual).Select(a => a.ClienteId).ToList();
            var idDataServicoFinalizado = agendas.Where(a => a.DataHora < DateTime.Now.Date).Select(a => a.ClienteId).ToList();
            //var idHoraServicoFinalizado = agendas.Where(a => a.Hora )

            if (idDataServicoFinalizado.Count > 0)
            {
                foreach (var itemId in idDataServicoFinalizado)
                {
                    //Chamar Delete
                    await DeleteConfirmed(itemId);
                }
            }
            else if (idDataHoraServicoFinalizado.Count > 0)
            {
                foreach (var itemId in idDataHoraServicoFinalizado)
                {
                    //Chamar Delete
                    await DeleteConfirmed(itemId);
                }
            }
        }

        // POST: Agenda2/Delete/5
        [HttpPost, ActionName("DeletarAgendamento")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Agenda agenda = await ctx.Agenda.FindAsync(id);
            ctx.Agenda.Remove(agenda);
            ctx.SaveChanges();
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Agenda/ListaAgendados");
            }
            else
            {
                return RedirectToAction("ListaAgendadosAdm");
            }            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

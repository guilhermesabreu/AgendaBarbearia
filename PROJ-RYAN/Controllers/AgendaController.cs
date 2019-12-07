﻿using System;
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
        public async Task<ActionResult> ListaAgendadosCliente()
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
                        Hora = agendamento.Hora
                    };
                    // Adcionando os novos valores populados na variável que representa as prop. da VM
                    propriedadesDaModelVM.Add(x);
                }

                var diasAgendadosComDistinct = ctx.Agenda.Select(x => x.DataHora.Day + "/" + x.DataHora.Month + "/" + x.DataHora.Year).OrderBy(x => x).ToList().Distinct();
                var diasAgendadosSemDistinct = ctx.Agenda.Select(x => x.DataHora.Day + "/" + x.DataHora.Month + "/" + x.DataHora.Year).ToList();

                ViewBag.DiaComDistinct = diasAgendadosComDistinct;
                ViewBag.DiaSemDistinct = diasAgendadosSemDistinct;

                //Retornando as propriedades da VM já populadas com a da Entidade
                return View(propriedadesDaModelVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListaAgendadosAdm()
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/LoginCadastro/Index");
            }
            else
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
                            Hora = agendamento.Hora
                        };
                        // Adcionando os novos valores populados na variável que representa as prop. da VM
                        propriedadesDaModelVM.Add(x);
                    }

                    var diasAgendadosComDistinct = ctx.Agenda.Select(x => x.DataHora.Day + "/" + x.DataHora.Month + "/" + x.DataHora.Year).OrderBy(x => x).ToList().Distinct();
                    var diasAgendadosSemDistinct = ctx.Agenda.Select(x => x.DataHora.Day + "/" + x.DataHora.Month + "/" + x.DataHora.Year).ToList();

                    ViewBag.DiaComDistinct = diasAgendadosComDistinct;
                    ViewBag.DiaSemDistinct = diasAgendadosSemDistinct;

                    //Retornando as propriedades da VM já populadas com a da Entidade
                    return View(propriedadesDaModelVM);
                }
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

        // GET: Agenda2/Details/5
        public ActionResult DetalhesCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = ctx.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda2/Create
        public ActionResult AgendarCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgendarCliente(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (ctx)
                {
                    var agenda = new Agenda
                    {
                        ClienteId = viewModel.ClienteId,
                        Nome = viewModel.Nome,
                        Email = viewModel.Email,
                        DataHora = viewModel.Data,
                        Hora = viewModel.Hora
                    };

                    if (!string.IsNullOrEmpty(viewModel.Nome))
                    {
                        var dataClienteCadastrado = ctx.Agenda.Where(a => a.DataHora == agenda.DataHora && a.Hora == agenda.Hora).FirstOrDefault();
                        var clienteCadastrado = ctx.Agenda.Where(a => a.Nome == agenda.Nome).FirstOrDefault();

                        if (clienteCadastrado == null)
                        {
                            TimeSpan minutos0 = new TimeSpan(09, 30, 0);
                            TimeSpan minutos1 = new TimeSpan(10, 20, 0);
                            TimeSpan minutos2 = new TimeSpan(11, 10, 0);
                            TimeSpan minutos3 = new TimeSpan(12, 00, 0);
                            TimeSpan minutos4 = new TimeSpan(12, 50, 0);
                            TimeSpan minutos5 = new TimeSpan(13, 40, 0);
                            TimeSpan minutos6 = new TimeSpan(14, 30, 0);
                            TimeSpan minutos7 = new TimeSpan(15, 20, 0);
                            TimeSpan minutos8 = new TimeSpan(16, 10, 0);
                            TimeSpan minutos9 = new TimeSpan(17, 00, 0);
                            TimeSpan minutos10 = new TimeSpan(17, 50, 0);
                            TimeSpan minutos11 = new TimeSpan(18, 40, 0);

                            List<TimeSpan> times = new List<TimeSpan>();
                            times.Add(minutos0);
                            times.Add(minutos1);
                            times.Add(minutos2);
                            times.Add(minutos3);
                            times.Add(minutos4);
                            times.Add(minutos5);
                            times.Add(minutos6);
                            times.Add(minutos7);
                            times.Add(minutos8);
                            times.Add(minutos9);
                            times.Add(minutos10);
                            times.Add(minutos11);
                            var horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

                            if (dataClienteCadastrado == null)
                            {
                                if (times.Contains(agenda.Hora.Value))
                                {
                                    if (agenda.DataHora > DateTime.Now.Date)
                                    {
                                        ctx.Agenda.Add(agenda);
                                        await ctx.SaveChangesAsync();
                                    }
                                    else if (agenda.DataHora == DateTime.Now.Date && TimeSpan.Compare(agenda.Hora.Value, horaAtual) == 1)
                                    {
                                        ctx.Agenda.Add(agenda);
                                        await ctx.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        ViewBag.DataAnterior = "Só é possível Cadastrar Datas do dia de Hoje em diante";
                                        return View();
                                    }
                                }
                                else
                                {
                                    ViewBag.HoraInvalida = "Escolha apenas nossos horários de serviço. das 09:30 até as 19:30";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.DataIndisponivel = "Esta data/hora já foi agendada, escolha outra data";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.NomeInvalido = "Você já foi cadastrado, verifique seu nome e Horário na nossa Agenda";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.NomeNaoInformado = "Digite seu nome Completo";
                        return View();
                    }
                }
            }
            return RedirectToAction("ListaAgendadosCliente");
        }

        public ActionResult AgendarClienteAdm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgendarClienteAdm(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (ctx)
                {
                    var agenda = new Agenda
                    {
                        ClienteId = viewModel.ClienteId,
                        Nome = viewModel.Nome,
                        Email = viewModel.Email,
                        DataHora = viewModel.Data,
                        Hora = viewModel.Hora
                    };

                    if (!string.IsNullOrEmpty(viewModel.Nome))
                    {
                        var dataClienteCadastrado = ctx.Agenda.Where(a => a.DataHora == agenda.DataHora && a.Hora == agenda.Hora).FirstOrDefault();
                        var clienteCadastrado = ctx.Agenda.Where(a => a.Nome == agenda.Nome).FirstOrDefault();

                        if (clienteCadastrado == null)
                        {
                            TimeSpan minutos0 = new TimeSpan(09, 30, 0);
                            TimeSpan minutos1 = new TimeSpan(10, 20, 0);
                            TimeSpan minutos2 = new TimeSpan(11, 10, 0);
                            TimeSpan minutos3 = new TimeSpan(12, 00, 0);
                            TimeSpan minutos4 = new TimeSpan(12, 50, 0);
                            TimeSpan minutos5 = new TimeSpan(13, 40, 0);
                            TimeSpan minutos6 = new TimeSpan(14, 30, 0);
                            TimeSpan minutos7 = new TimeSpan(15, 20, 0);
                            TimeSpan minutos8 = new TimeSpan(16, 10, 0);
                            TimeSpan minutos9 = new TimeSpan(17, 00, 0);
                            TimeSpan minutos10 = new TimeSpan(17, 50, 0);
                            TimeSpan minutos11 = new TimeSpan(18, 40, 0);

                            List<TimeSpan> times = new List<TimeSpan>();
                            times.Add(minutos0);
                            times.Add(minutos1);
                            times.Add(minutos2);
                            times.Add(minutos3);
                            times.Add(minutos4);
                            times.Add(minutos5);
                            times.Add(minutos6);
                            times.Add(minutos7);
                            times.Add(minutos8);
                            times.Add(minutos9);
                            times.Add(minutos10);
                            times.Add(minutos11);
                            var horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

                            if (dataClienteCadastrado == null)
                            {
                                if (times.Contains(agenda.Hora.Value))
                                {
                                    if (agenda.DataHora > DateTime.Now.Date)
                                    {
                                        ctx.Agenda.Add(agenda);
                                        await ctx.SaveChangesAsync();
                                    }
                                    else if (agenda.DataHora == DateTime.Now.Date && TimeSpan.Compare(agenda.Hora.Value, horaAtual) == 1)
                                    {
                                        ctx.Agenda.Add(agenda);
                                        await ctx.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        ViewBag.DataAnterior = "Só é possível Cadastrar Datas do dia de Hoje em diante";
                                        return View();
                                    }
                                }
                                else
                                {
                                    ViewBag.HoraInvalida = "Escolha apenas nossos horários de serviço. das 09:30 até as 19:30";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.DataIndisponivel = "Esta data/hora já foi agendada, escolha outra data";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.NomeInvalido = "Você já foi cadastrado, verifique seu nome e Horário na nossa Agenda";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.NomeNaoInformado = "Digite seu nome Completo";
                        return View();
                    }
                }
            }
            return RedirectToAction("ListaAgendadosAdm");
        }
        // GET: Agenda2/Edit/5
        public async Task<ActionResult> AtualizarDadosClientes(int? id)
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/LoginCadastro/Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Agenda agenda = await ctx.Agenda.FindAsync(id);
                if (agenda == null)
                {
                    return HttpNotFound();
                }
                return View(agenda);
            }
        }

        // POST: Agenda2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AtualizarDadosClientes(Agenda agenda)
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/LoginCadastro/Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ctx.Entry(agenda).State = EntityState.Modified;
                    await ctx.SaveChangesAsync();
                    return RedirectToAction("ListaAgendados");
                }
                return View(agenda);
            }
        }

        // GET: Agenda2/Delete/5
        public async Task<ActionResult> DeletarAgendamento(int? id)
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/LoginCadastro/Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Agenda agenda = await ctx.Agenda.FindAsync(id);
                if (agenda == null)
                {
                    return HttpNotFound();
                }
                return View(agenda);
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
            return RedirectToAction("ListaAgendados");
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

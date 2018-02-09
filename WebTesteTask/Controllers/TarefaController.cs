using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTask.Controllers
{
    public class TarefaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnviarEmail(string destino, string assunto, string corpo)
        {
            for (int i = 0; i < 2; i++)
            {
                BackgroundJob.Enqueue<Email>(x => x.SendAsync(new IdentityMessage { Destination = destino, Body = corpo, Subject = assunto }));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Integracao(string nome, object dados)
        {
            var corpo = dados;
            for (int i = 0; i < 2; i++)
            {
                BackgroundJob.Enqueue<Email>(x => x.SendAsync(new IdentityMessage { Destination = "eder.lopes@live.com", Body = "configure o corpo do email", Subject = nome }));
            }
            return Content(Request.QueryString["hub.challenge"] == null ? "" : Request.QueryString["hub.challenge"].ToString());
        }
	}
}
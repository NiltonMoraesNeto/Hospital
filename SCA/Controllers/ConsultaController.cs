using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SCA.Bll;
using SCA.Model;
using SCA.Models;
using SCA.Models.ENUMS;
using System.Net.Mail;


namespace SCA.Controllers
{
    public class ConsultaController : BaseController
    {
        // GET: Consulta
        public ActionResult Index()
        {
            List<Consulta> list = new List<Consulta>();
            try
            {
                var bll = new ConsultaBll();
                list = bll.GetList(" Where Finalizar = 1 ORDER BY IdConsulta");

                return View(list);
            }
            catch (Exception ex)
            {
                Danger("Erro", "Erro: " + ex.Message, true);
                return View(list);
            }
        }


        // GET: Home
        [AccessDeniedAuthorize]
        public ActionResult Fechado()
        {
            List<Consulta> list = new List<Consulta>();
            try
            {
                var bll = new ConsultaBll();
                list = bll.GetList(" Where Finalizar = 1 ORDER BY IdConsulta");

                return View(list);
            }
            catch (Exception ex)
            {
                Danger("Erro", "Erro: " + ex.Message, true);
                return View(list);
            }

        }
        

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private void ObjectsList(Consulta consulta)
        {

            try
            {
                var pacientesBll = new PacientesBll();
                var listpacientes = pacientesBll.GetList();
                ViewBag.Pacientes = new SelectList(listpacientes, "IdPaciente", "NomePaciente");

                var usuarioBll = new UsuariosBll();
                var listusuario = usuarioBll.GetList(" WHERE IdPerfil = 2");
                ViewBag.Usuarios = new SelectList(listusuario, "IdUsuario", "NomeUsuario");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            var consulta = new Consulta();
            ObjectsList(consulta);
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        public ActionResult Create(Consulta consulta, FormCollection collection)
        {
            try
            {
                consulta.Pacientes = new Pacientes(Convert.ToInt32(collection["Pacientes"]));
                consulta.Usuarios = new Usuarios(Convert.ToInt32(collection["Usuarios"]));
                consulta.Titulo = collection["Titulo"];

                var bll = new ConsultaBll();
                bll.Save(consulta);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            var consulta = new Consulta();

            var bll = new ConsultaBll();
            consulta = bll.GetObject(id);

            var consulta1 = new Consulta();
            ObjectsList(consulta1);

            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Consulta consulta, FormCollection collection)
        {
            try
            {
                consulta.Pacientes = new Pacientes(Convert.ToInt32(collection["Pacientes"]));
                consulta.Usuarios = new Usuarios(Convert.ToInt32(collection["Usuarios"]));
                consulta.Titulo = collection["Titulo"];

                var bll = new ConsultaBll();
                bll.Save(consulta);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            var consulta = new Consulta();

            var bll = new ConsultaBll();
            consulta = bll.GetObject(id);

            var consulta1 = new Consulta();
            ObjectsList(consulta1);

            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Consulta consulta = new Consulta(id);
                ConsultaBll bll = new ConsultaBll();
                bll.Delete(consulta);

                Success("Sucesso", "Excluido com sucesso!", true);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}

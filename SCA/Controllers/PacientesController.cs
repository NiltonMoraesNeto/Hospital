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
    public class PacientesController : BaseController
    {
        // GET: Pacientes
        public ActionResult Index()
        {
            List<Pacientes> list = new List<Pacientes>();
            try
            {
                var bll = new PacientesBll();
                list = bll.GetList(" ORDER BY NomePaciente");

                return View(list);
            }
            catch (Exception ex)
            {
                Danger("Erro", "Erro: " + ex.Message, true);
                return View(list);
            }
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private void ObjectsList(Pacientes pacientes)
        {

            try
            {
                var licencaBll = new LicencasBll();
                var listlicenca = licencaBll.GetList();
                ViewBag.Licencas = new SelectList(listlicenca, "IdLicenca", "Descricao");

                var planoSaudeBll = new PlanoSaudeBll();
                var listplanoSaude = planoSaudeBll.GetList();
                ViewBag.PlanoSaude = new SelectList(listplanoSaude, "IdPlanoSaude", "Descricao");

                var usuarioBll = new UsuariosBll();
                var listusuario = usuarioBll.GetList(" WHERE IdPerfil = 2");
                ViewBag.Usuarios = new SelectList(listusuario, "IdUsuario", "NomeUsuario");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            var pacientes = new Pacientes();
            ObjectsList(pacientes);
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        public ActionResult Create(Pacientes pacientes, FormCollection collection)
        {
            try
            {
                pacientes.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                pacientes.PlanoSaude = new PlanoSaude(Convert.ToInt32(collection["PlanoSaude"]));
                pacientes.Usuarios = new Usuarios(Convert.ToInt32(collection["Usuarios"]));


                pacientes.NomePaciente = collection["NomePaciente"];

                var bll = new PacientesBll();
                bll.Save(pacientes);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int id)
        {
            var pacientes = new Pacientes();

            var bll = new PacientesBll();
            pacientes = bll.GetObject(id);

            var pacientes1 = new Pacientes();
            ObjectsList(pacientes1);

            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pacientes pacientes, FormCollection collection)
        {
            try
            {
                pacientes.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                pacientes.PlanoSaude = new PlanoSaude(Convert.ToInt32(collection["PlanoSaude"]));
                pacientes.Usuarios = new Usuarios(Convert.ToInt32(collection["Usuarios"]));

                pacientes.NomePaciente = collection["NomePaciente"];

                var bll = new PacientesBll();
                bll.Save(pacientes);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int id)
        {
            var pacientes = new Pacientes();

            var bll = new PacientesBll();
            pacientes = bll.GetObject(id);

            var pacientes1 = new Pacientes();
            ObjectsList(pacientes1);

            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pacientes pacientes = new Pacientes(id);
                PacientesBll bll = new PacientesBll();
                bll.Delete(pacientes);

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

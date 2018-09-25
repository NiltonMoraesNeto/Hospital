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
    public class ModeloController : BaseController
    {
        // GET: Modelo
        public ActionResult Index()
        {
            List<Modelo> list = new List<Modelo>();
            try
            {
                var bll = new ModeloBll();
                list = bll.GetList(" ORDER BY Descricao");

                return View(list);
            }
            catch (Exception ex)
            {
                Danger("Erro", "Erro: " + ex.Message, true);
                return View(list);
            }
        }

        // GET: Modelo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private void ObjectsList(Modelo modelo)
        {

            try
            {
                var licencaBll = new LicencasBll();
                var listlicenca = licencaBll.GetList();
                ViewBag.Licencas = new SelectList(listlicenca, "IdLicenca", "Descricao");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Modelo/Create
        public ActionResult Create()
        {
            var modelo = new Modelo();
            ObjectsList(modelo);
            return View();
        }

        // POST: Modelo/Create
        [HttpPost]
        public ActionResult Create(Modelo modelo, FormCollection collection)
        {
            try
            {
                modelo.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                modelo.Descricao = collection["Descricao"].ToUpper();

                var bll = new ModeloBll();
                bll.Save(modelo);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Modelo/Edit/5
        public ActionResult Edit(int id)
        {
            var modelo = new Modelo();

            var bll = new ModeloBll();
            modelo = bll.GetObject(id);

            var modelo1 = new Modelo();
            ObjectsList(modelo1);

            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: Modelo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Modelo modelo, FormCollection collection)
        {
            try
            {
                modelo.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                modelo.Descricao = collection["Descricao"].ToUpper();

                var bll = new ModeloBll();
                bll.Save(modelo);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Modelo/Delete/5
        public ActionResult Delete(int id)
        {
            var modelo = new Modelo();

            var bll = new ModeloBll();
            modelo = bll.GetObject(id);

            var modelo1 = new Modelo();
            ObjectsList(modelo1);

            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: Modelo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Modelo modelo = new Modelo(id);
                ModeloBll bll = new ModeloBll();
                bll.Delete(modelo);

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

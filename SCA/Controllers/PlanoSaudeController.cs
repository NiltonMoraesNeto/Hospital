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
    public class PlanoSaudeController : BaseController
    {
        // GET: PlanoSaude
        public ActionResult Index()
        {
            List<PlanoSaude> list = new List<PlanoSaude>();
            try
            {
                var bll = new PlanoSaudeBll();
                list = bll.GetList(" ORDER BY Descricao");

                return View(list);
            }
            catch (Exception ex)
            {
                Danger("Erro", "Erro: " + ex.Message, true);
                return View(list);
            }
        }

        // GET: PlanoSaude/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private void ObjectsList(PlanoSaude planoSaude)
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

        // GET: PlanoSaude/Create
        public ActionResult Create()
        {
            var planoSaude = new PlanoSaude();
            ObjectsList(planoSaude);
            return View();
        }

        // POST: PlanoSaude/Create
        [HttpPost]
        public ActionResult Create(PlanoSaude planoSaude, FormCollection collection)
        {
            try
            {
                planoSaude.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                planoSaude.Descricao = collection["Descricao"].ToUpper();

                var bll = new PlanoSaudeBll();
                bll.Save(planoSaude);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: PlanoSaude/Edit/5
        public ActionResult Edit(int id)
        {
            var planoSaude = new PlanoSaude();

            var bll = new PlanoSaudeBll();
            planoSaude = bll.GetObject(id);

            var planoSaude1 = new PlanoSaude();
            ObjectsList(planoSaude1);

            if (planoSaude == null)
            {
                return HttpNotFound();
            }
            return View(planoSaude);
        }

        // POST: PlanoSaude/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PlanoSaude planoSaude, FormCollection collection)
        {
            try
            {
                planoSaude.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));
                planoSaude.Descricao = collection["Descricao"].ToUpper();

                var bll = new PlanoSaudeBll();
                bll.Save(planoSaude);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: PlanoSaude/Delete/5
        public ActionResult Delete(int id)
        {
            var planoSaude = new PlanoSaude();

            var bll = new PlanoSaudeBll();
            planoSaude = bll.GetObject(id);

            var planoSaude1 = new PlanoSaude();
            ObjectsList(planoSaude1);

            if (planoSaude == null)
            {
                return HttpNotFound();
            }
            return View(planoSaude);
        }

        // POST: PlanoSaude/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PlanoSaude planoSaude = new PlanoSaude(id);
                PlanoSaudeBll bll = new PlanoSaudeBll();
                bll.Delete(planoSaude);

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

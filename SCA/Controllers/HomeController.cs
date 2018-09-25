using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.UI.WebControls.Expressions;
using SCA.Bll;
using SCA.Dal;
using SCA.Model;
using SCA.Models;
using SCA.Models.ENUMS;
using System.Web.Helpers;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SCA.Controllers
{
    public class HomeController : BaseController
    {

        

        // GET: Home
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            //if (SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil == 1 || SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil == 2)
            //{

            //    return View("Index");
            //}

            //if (SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil == 5)
            //{
            //    RedirectToAction("Pumatronix", "Os");
            //}

            ////return View("Index2");
            //return RedirectToAction("Pumatronix", "Os");

            
            var bll = new ConsultaBll();
            //var bll2 = new PacientesBll();

            var retornarContato = bll.GetList();
            //var retornarContato2 = bll2.GetList();

            ViewBag.ListMes = retornarContato;
            //ViewBag.ListMes2 = retornarContato2;

            return View("Index");
        }
        

        // GET: Home/Details/5
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        //[WebMethod()]

        //public static List

        // GET: Home/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();

        }

        private List<Consulta> GetListMes(int mes, int ano, List<Consulta> list)
        {
            DateTime PrimeiroDiadoMes = DateTime.Parse(ano + "/" + mes + "/01");
            DateTime PrimeiroDiadoProximoMes = PrimeiroDiadoMes.AddMonths(1);
            DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

            var listMes = new List<Consulta>();

            for (int i = 1; i <= UltimoDiadoMes.Day; i++)
            {
                var listDia = new List<Consulta>();
                listDia = list.Where(o => o.DataConsulta.Value.Day.Equals(i)).ToList();
                for (int j = 0; j < listDia.Count(); j++)
                {
                    listMes.Add(listDia[j]);
                }
            }



            return listMes;

        }
    }
}

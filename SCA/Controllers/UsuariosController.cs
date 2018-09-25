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
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace SCA.Controllers
{
    public class UsuariosController : BaseController
    {
        private string rowWidth = "";
        //[AccessDeniedAuthorizeAttribute]
        //[AccessDeniedAuthorizeAttribute(Roles = "Administrador,Funcionário")]
        // GET: Usuarios
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            var usuarios = new List<Usuarios>();
            var bll = new UsuariosBll();

            string sql = "";

            usuarios = bll.GetList(sql, true);

            return View(usuarios);
        }

        public JsonResult Relatorio(string dataInicio, string dataFim)
        {

            var bll = new UsuariosBll();
            var acompanhar = new List<Usuarios>();

            string sql = "";
            int idUsuario = SessionContext.UsuarioLogado.Usuarios.IdUsuario;

            var list = bll.GetList(sql);
           

            if (!String.IsNullOrEmpty(dataInicio))
            {
                DateTime inicio = DateTime.Parse(dataInicio);
                sql += " and OP.DataProposta >= '" + inicio.ToString("yyyy-MM-dd") + " 00:00:00' ";
            }
            if (!String.IsNullOrEmpty(dataFim))
            {
                DateTime fim = DateTime.Parse(dataFim);
                sql += " and OP.DataProposta <= '" + fim.ToString("yyyy-MM-dd") + " 23:59:59' ";
            }

            //var list = bll.GetList(sql);

            return Json(new { url = CreateExcel(list) }, JsonRequestBehavior.AllowGet);
        }

        private string CreateExcel(List<Usuarios> list)
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);

            ws.Name = "Usuários";
            int inicioRow = 5;
            int inicioColumn = 1;

            int inicioRowA = 1;
            int inicioColumnA = 1;

            int inicioRowB = 2;
            int inicioColumnB = 1;

            int inicioRowC = 3;
            int inicioColumnC = 1;
            var dataAtual = DateTime.Now;

            int inicioRowD = 4;
            int inicioColumnD = 1;
            //int idUsuario = SessionContext.UsuarioLogado.Usuarios.IdUsuario;
            var nomeUsuario = SessionContext.UsuarioLogado.Usuarios.NomeUsuario;

            /*INICIO - HEAD*/
            rowWidth = "15";
            SetRangeExcelHead2(ws, inicioRowA, inicioColumnA++, "Licença", "#FFF", "", "9.13");
            SetRangeExcelHead2(ws, inicioRowA, inicioColumnA++, "NILTON MORAES NETO", "#FFF", "", "8.88");
            SetRangeExcelHead2(ws, inicioRowB, inicioColumnB++, "Documento", "#FFF", "", "9.13");
            SetRangeExcelHead2(ws, inicioRowB, inicioColumnB++, "USUÁRIOS CADASTRADOS", "#FFF", "", "8.88");
            SetRangeExcelHead2(ws, inicioRowC, inicioColumnC++, "Data de Impressão", "#FFF", "", "9.13");
            SetRangeExcelHead2(ws, inicioRowC, inicioColumnC++, dataAtual.ToString(), "#FFF", "", "8.88");
            SetRangeExcelHead2(ws, inicioRowD, inicioColumnD++, "Usuário", "#FFF", "", "9.13");
            SetRangeExcelHead2(ws, inicioRowD, inicioColumnD++, nomeUsuario, "#FFF", "", "8.88");
            rowWidth = "48";
            SetRangeExcelHead2(ws, inicioRow, inicioColumn++, "", "#FFF", "#000", "12");
            SetRangeExcelHead(ws, inicioRow, inicioColumn++, "NOME", "#FF0000", "#FFFFFF", "21");
            SetRangeExcelHead(ws, inicioRow, inicioColumn++, "LOGIN", "#FF0000", "#FFFFFF", "54");
            SetRangeExcelHead(ws, inicioRow, inicioColumn++, "EMAIL", "#FF0000", "#FFFFFF", "12.75");

            inicioRow++;
            inicioColumn = 1;
            inicioRowA++;
            inicioColumnA = 1;
            inicioRowB++;
            inicioColumnB = 1;
            inicioRowC++;
            inicioColumnC = 1;
            inicioRowD++;
            inicioColumnD = 1;

            /*FIM - HEAD*/

            rowWidth = "24";

            for (int i = 0; i < list.Count; i++)
            {
                int linha = i + 6;
                inicioColumn = 2;


                SetRangeExcelBody(ws, linha, inicioColumn++, list[i].NomeUsuario);
                SetRangeExcelBody(ws, linha, inicioColumn++, list[i].Login);
                SetRangeExcelBody(ws, linha, inicioColumn++, list[i].Email);
                //SetRangeExcelBody(ws, linha, inicioColumn++, HasDate(list[i].DataPrevisaoEntrega));
                //SetRangeExcelBody(ws, linha, inicioColumn++, HasDate(list[i].DataEntregaConfirTransp));
                //SetRangeExcelBody(ws, linha, inicioColumn++, HasDate(list[i].DataPrevistaPortabilidade));
                //SetRangeExcelBody(ws, linha, inicioColumn++, HasDate(list[i].DataConfirmacaoEntreguePortab));
                //SetRangeExcelBody(ws, linha, inicioColumn++, list[i].Concluido);
            }


            string nameFile = ("/Reports/Relatorio/Relatorio_") + Guid.NewGuid().ToString() + ".xlsx";
            string filePath = Server.MapPath(nameFile);
            //string nameFile = "c:\\temp\\rel_" + Guid.NewGuid().ToString() + ".xlsx";

            wb.SaveAs(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            wb.Close();
            excel.Quit();

            return nameFile;

        }

        private Range SetRangeExcelHead(Worksheet ws, int linha, int coluna, string value, string backColor = "#f2f2f2",
string fontColor = "#000", string columnWidth = "20", bool lineBreak = false)
        {
            Microsoft.Office.Interop.Excel.Range ce = (ws.Cells[linha, coluna] as Microsoft.Office.Interop.Excel.Range);
            ce.Value2 = value;
            ce.Interior.Color = ColorTranslator.FromHtml(backColor);
            ce.Font.Color = ColorTranslator.FromHtml(fontColor);
            ce.Borders.Color = Color.Black;
            ce.ColumnWidth = columnWidth;
            ce.RowHeight = rowWidth;
            ce.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ce.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ce.Font.Size = 9;
            //ce.Response.Write("\"aa\rb\rccc\"");
            //ce.WrapText = true;
            //ce.EntireRow.RowHeight = 33;
            //ce.EntireColumn.AutoFit();
            //ce.AutoFit();

            return ce;
        }
        private Range SetRangeExcelHead2(Worksheet ws, int linha, int coluna, string value, string backColor = "#f2f2f2",
    string fontColor = "#000", string columnWidth = "20", bool lineBreak = false)
        {
            Microsoft.Office.Interop.Excel.Range ce = (ws.Cells[linha, coluna] as Microsoft.Office.Interop.Excel.Range);
            ce.Value2 = value;
            ce.Interior.Color = ColorTranslator.FromHtml(backColor);
            ce.Font.Color = ColorTranslator.FromHtml(fontColor);
            ce.Borders.Color = Color.Gainsboro;
            ce.ColumnWidth = columnWidth;
            ce.RowHeight = rowWidth;
            ce.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ce.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ce.Font.Size = 9;

            return ce;
        }

        private Range SetRangeExcelBody(Worksheet ws, int linha, int coluna, string value,
    XlHAlign horizontalAlignment = XlHAlign.xlHAlignCenter)
        {
            Microsoft.Office.Interop.Excel.Range ce = (ws.Cells[linha, coluna] as Microsoft.Office.Interop.Excel.Range);
            ce.Value2 = String.IsNullOrEmpty(value) ? value : value.ToUpper();
            ce.Borders.Color = Color.Black;
            ce.RowHeight = rowWidth;
            ce.HorizontalAlignment = horizontalAlignment;
            ce.VerticalAlignment = XlVAlign.xlVAlignCenter;
            ce.Font.Size = 9;

            return ce;
        }

        // GET: Usuarios/Details/5
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        private void ObjectsList(Usuarios usuarios)
        {
            try
            {

                var licencasBll = new LicencasBll();
                var listlicencas = licencasBll.GetList();
                ViewBag.Licencas = new SelectList(listlicencas, "IdLicenca", "Descricao");

                //var usuarioBll = new UsuariosBll();
                //var listUsuarios = usuarioBll.GetList(" where idusuario not in(" + IdUsuario + ") and excluido = 0 ", false);
                //ViewBag.Usuarios = new SelectList(listUsuarios, "IdUsuario", "NomeUsuario");

                var perfilBll = new UsuariosperfisBll();
                var listPerfis = perfilBll.GetList().Where(x => x.IdPerfil >= SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil);
                ViewBag.Perfis = new SelectList(listPerfis, "IdPerfil", "Nome");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // GET: Usuarios/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            //ObjectsList(0);
            //return View();

            var usuarios = new Usuarios();
            ObjectsList(usuarios);
            return View();
        }

        // POST: Usuarios/Create
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Create(Usuarios usuarios, FormCollection collection)
        {
            try
            {
                usuarios.NomeUsuario = collection["NomeUsuario"];
                usuarios.Perfil = new Usuariosperfis(Convert.ToInt32(collection["Perfil"]));
                //usuarios.Licencas = new Usuarioslicencas(Convert.ToInt32(collection["Licencas"]));

                Usuarioslicencas usuarioslicencas = new Usuarioslicencas();
                usuarioslicencas.Usuarios = usuarios;
                usuarioslicencas.Usuarios.IdUsuario = usuarios.IdUsuario;
                usuarioslicencas.Status = usuarios.Status;
                usuarioslicencas.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));

                usuarios.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));


                usuarios.Email = collection["Email"];


                var bll = new UsuariosBll();
                bll.Save(usuarios);

                var bllUL = new UsuarioslicencasBll();
                usuarioslicencas.Usuarios = new Usuarios(usuarios.IdUsuario);
                bllUL.Save(usuarioslicencas);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {

            var usuarios = new Usuarios();

            var bll = new UsuariosBll();
            usuarios = bll.GetObject(id);

            var os1 = new Usuarios();
            ObjectsList(os1);

            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);


            //ObjectsList(id);

            //var usuarios = new Usuarios();

            //var bll = new UsuariosBll();

            //var usuario = new Usuarios(id);

            //usuarios = bll.GetObject(usuario);

            //if (usuarios == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(usuarios);

            //var bll = new UsuariosBll();
            //var usuario = bll.GetObject(id);




            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Edit(int id, Usuarios usuarios, FormCollection collection)
        {
            try
            {
                usuarios.IdUsuario = (Convert.ToInt32(collection["IdUsuario"]));

                //Usuarioslicencas usu = new Usuarioslicencas();

                usuarios.Perfil = new Usuariosperfis(Convert.ToInt32(collection["Perfil"]));

                Usuarioslicencas usuarioslicencas = new Usuarioslicencas();
                usuarioslicencas.Usuarios = usuarios;
                usuarioslicencas.Usuarios.IdUsuario = usuarios.IdUsuario;
                usuarioslicencas.Status = usuarios.Status;
                usuarioslicencas.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));

                usuarios.Licencas = new Licencas(Convert.ToInt32(collection["Licencas"]));


                usuarios.Email = collection["Email"];

                var bll = new UsuariosBll();
                bll.Save(usuarios);

                var bllUL = new UsuarioslicencasBll();
                usuarioslicencas.Usuarios = new Usuarios(usuarios.IdUsuario);
                bllUL.Save(usuarioslicencas);


                //var bllUL = new UsuarioslicencasBll();
                //usuarios.IdUsuarioLicenca.Usuarios = new Usuarios(usuarios.IdUsuario);
                //bllUL.Save(usuarios);

                Success("Sucesso", "Alterado com sucesso!", true);

                return RedirectToAction("/");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format(ex.Message), true);
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                UsuariosBll bll = new UsuariosBll();
                Usuarios usuarioLogin = new Usuarios();
                usuarioLogin.Persisted = true;
                //usuarioLogin.Email = collection["login"];
                //usuarioLogin.CPF = collection["cpf"];
                usuarioLogin.Login = collection["Login"];
                usuarioLogin.Senha = collection["password"].Trim();

                var usuario = bll.Login(usuarioLogin);

                var licencaBll = new UsuarioslicencasBll();
                Usuarioslicencas licenca = null;

                if (usuario.Perfil.Nome.ToLower() == "Administrador")
                {
                    //licenca = new Usuarioslicencas()
                    //{
                    //    Funcao = new Licencasfuncoes() { Nome = "Desenvolvedor" },
                    //    Status = StatusUsuarioLicenca.Ativo,
                    //    Licencas = new Licencas() { Descricao = "Desenvolvedor" },
                    //    Usuarios = usuario
                    //};
                    licenca = licencaBll.GetObject(usuario);
                }
                else
                {
                    licenca = licencaBll.GetObject(usuario);


                    if (licenca == null)
                    {
                        throw new Exception("Atenção! Seu usuário não possui uma licença valida.");
                    }
                    else
                    {
                        if (licenca.Status != StatusUsuarioLicenca.Ativo)
                        {
                            throw new Exception("Atenção! Sua licença não está ativa.");
                        }


                    }

                }



                SessionContext.UsuarioLogado = licenca;
                FormsAuthentication.SetAuthCookie(usuario.Login, false);


                if (SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Danger("Erro", ex.Message, true);

                return View();
            }
        }


        public ActionResult LogOff()
        {
            SessionContext.UsuarioLogado = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Usuarios");
        }

    }
}

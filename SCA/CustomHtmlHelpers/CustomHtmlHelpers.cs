using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Model;
using SCA.Models.Extensions;
using SCA.Models;

namespace SCA.CustomHtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        private static TagBuilder DivNavMid(string id, string _class, string style, string icon, string name)
        {
            var divNavMid = new TagBuilder("div");
            divNavMid.Attributes.Add("id", id);
            divNavMid.AddCssClass(_class);
            divNavMid.Attributes.Add("style", style);

            var i = new TagBuilder("i");
            i.AddCssClass("glyphicon " + icon);

            var span = new TagBuilder("span");
            span.SetInnerText(name);

            divNavMid.InnerHtml += i.ToString(TagRenderMode.Normal);
            divNavMid.InnerHtml += span.ToString(TagRenderMode.Normal);

            return divNavMid;
        }



        public static MvcHtmlString MenuSistema(this HtmlHelper helper)
        {
            int tipoUsuario = 0;
            if (SessionContext.UsuarioLogado != null)
            {
                if (SessionContext.UsuarioLogado.Usuarios != null)
                {
                    if (SessionContext.UsuarioLogado.Usuarios.Perfil != null)
                    {
                        tipoUsuario = SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil;
                    }
                }
            }



            //INICIO
            var aside = new TagBuilder("aside");
            var sidebar = new TagBuilder("div");
            sidebar.Attributes.Add("id", "sidebar");
            sidebar.AddCssClass("nav-collapse");

            var sidebar_menu = new TagBuilder("ul");
            sidebar_menu.AddCssClass("sidebar-menu");
            sidebar_menu.MergeAttribute("style", "display: none1;", true);
            var sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            var tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");



            //INICIO MENU **CADASTROS**
            var tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_document_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            var span = new TagBuilder("span");
            span.InnerHtml += "Cadastros";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            var sub = new TagBuilder("ul");
            //sub.AddCssClass("list-auto");
            sub.AddCssClass("sub");





            var li_sub = new TagBuilder("li");

            var tagA_sub = new TagBuilder("a");

            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/Usuarios/Index/");
            tagA_sub.InnerHtml += "Usuários";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/Pacientes/Index/");
            tagA_sub.InnerHtml += "Pacientes";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/PlanoSaude/Index/");
            tagA_sub.InnerHtml += "Plano de Saúde";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);



            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **CADASTRO**

            //INICIO MENU **CONSULTA**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_plus");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Consultas";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Consulta/Index/");
            tagA_sub.InnerHtml += "Consultas - Aberto";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Consulta/Fechado/");
            tagA_sub.InnerHtml += "Consultas - Fechado";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **CONSULTA**





            sidebar.InnerHtml += sidebar_menu.ToString(TagRenderMode.Normal);
            aside.InnerHtml += sidebar.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(aside.ToString(TagRenderMode.Normal));
            //FIM
        }

        private static string HasDate(DateTime? date)
        {
            string d = "";
            if (date.HasValue)
                d = date.Value.ToShortDateString();

            return d;
        }

        private static string MoneyBR(decimal valor)
        {
            string d = "";
            d = valor.ToBrDecimal();

            return "R$ " + d;
        }


    }
}
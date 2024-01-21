using GerenciadorAcervo.Modelos.Apoio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;
using System.Net;
using System.Security.Claims;

namespace GerenciadorAcervo.API.Filtros
{
    public class ExceptionFilterCustomizado : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return Task.CompletedTask;
            else
            {
                string action = filterContext.RouteData.Values["action"].ToString();
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string usuarioLogado = filterContext.HttpContext.User.Identity?.Name;
                string email = string.Empty;

                if (!string.IsNullOrWhiteSpace(usuarioLogado))
                {
                    var claimsIdentity = (ClaimsIdentity)filterContext.HttpContext.User.Identity;
                    email = claimsIdentity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email).Value;
                }

                using (LogContext.PushProperty("Action", action))
                using (LogContext.PushProperty("Controller", controller))
                using (LogContext.PushProperty("UsuarioLogado", usuarioLogado))
                using (LogContext.PushProperty("Email", String.IsNullOrWhiteSpace(email) ? null : email))
                {
                    Log.Logger.Error(filterContext.Exception, "Erro interno de processamento");
                }
                
                filterContext.Result = new JsonResult(filterContext.Exception.Message);
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.CompletedTask;
        }
    }
}

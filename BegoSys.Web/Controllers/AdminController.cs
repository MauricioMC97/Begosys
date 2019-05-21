using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;
using BegoSys.Common.ProveedoresDependencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Web.Controllers
{
    public class AdminController : ControladorJsonNet
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
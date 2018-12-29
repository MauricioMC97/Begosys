using BegoSys.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Web.Controllers
{
    public class BillingController : ControladorJsonNet
    {
        /// <summary>
        /// Permite guardar un pedido nuevo
        /// </summary>
        /// <param name="DatosFactura">Datos de la factura y del detalle de la factura</param>
        /// <returns>Retorna OK si la operación es exitosa</returns>
        [HttpPost]
        public JsonResult GuardarPedido(FacturaTO DatosFactura)
        {
            _proxy.PostForObject<EntidadTo>(ConstantesApi.GuardarEntidadUri, DatosFactura);
            return Json(new { status = "ok" });
        }

        [HttpPost]
        public JsonResult ImprimirPedido(FacturaTO DatosFactura)
        {
            return Json(new { status = "ok" });
        }


        // GET: Facturacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facturacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Facturacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facturacion/Create
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

        // GET: Facturacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facturacion/Edit/5
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

        // GET: Facturacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Facturacion/Delete/5
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
    }
}

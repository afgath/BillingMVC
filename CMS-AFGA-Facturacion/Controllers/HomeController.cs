using CMS_AFGA_Facturacion.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_AFGA_Facturacion.Controllers
{
    public class HomeController : Controller
    {
        private Cliente cliente = new Cliente();
        private Factura_Detalle factura_detalle = new Factura_Detalle();
        private Factura_Encabezado factura_encabezado = new Factura_Encabezado();
        private Articulo articulo = new Articulo();

        public ActionResult Index()
        {
            return View(cliente.Listar());
        }

        public ActionResult Clientes()
        {
            return View();
        }

        //home/Facturas/id
        //Lista todas las facturas según el cliente
        public ActionResult Facturas(int id = 0)
        {

             //Listamos las facturas de un cliente
             ViewBag.ProductosElegidos = factura_encabezado.Listar(id);

             //Listamos todos los artículos DISPONIBLES
            ViewBag.Articulos = articulo.Todos();
            ViewBag.Cliente = id;
            //Modelo
            factura_encabezado.ClienteId = id;

            return View();
            
        }

        //Se creaelencabezado de una nueva factura
        public ActionResult NuevaFacturaEncabezado(int id = 0)
        {
            //Se asigna el id de cliente al encabezado
            factura_encabezado.ClienteId = id;
            //Se asigna fecha al encabezado
            DateTime dateTime = new DateTime();
            dateTime = DateTime.UtcNow.Date;
            factura_encabezado.Factura_EncabezadoFecha = dateTime;

            //Modelo
            CrearFacturaEncabezado(factura_encabezado);

            //Se toma el id del encabezado para redireccionar hacia la creación del detalle de la factura
            int EncabezadoId = factura_encabezado.Factura_EncabezadoId;

            return Redirect("~/home/NuevaFactura/"+ EncabezadoId);
        }


        //Carga la vista para crear una factura nueva
        public ActionResult NuevaFactura(int id = 0)
        {
            //Contador para el cálculo del total de cada factura
            decimal cont = 0;
            //Se asigna el encabezado a la factura
            factura_encabezado = factura_encabezado.Obtener(id);

            //Se envía el encabezado hidratado a la vista
            ViewBag.Factura_Encabezado = factura_encabezado;

            //Se envían las opciones de la factura a lavista
            ViewBag.facturas_detalle = factura_detalle.Listar(id);

            //Se calcula el total a pagar
            foreach (var factura in factura_detalle.Listar(id))
            {
                cont += factura.Articulo.ArticuloValor * factura.Factura_DetalleCantidad;
            }
            ViewBag.totalAPagar = cont;

            // Enviamos todos los cursos DISPONIBLES a la vista
            ViewBag.Articulos = articulo.Todos();


            // Modelo
            factura_detalle.Factura_EncabezadoId = factura_encabezado.Factura_EncabezadoId;
            factura_detalle.ArticuloId = articulo.ArticuloId;
            
            return View(factura_detalle);
        }

        //Método para guardar mediante AJAX el encabezado de la factura
        public JsonResult CrearFacturaEncabezado(Factura_Encabezado model)
        {
            
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();

                if (rm.response)
                {
                    rm.function = "pageRedirect()";
                }
            }

            return Json(rm);
        }

        //Método para guardar mediante AJAX el detalle de la factura
        public JsonResult CrearFactura(Factura_Detalle model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();

                if (rm.response)
                {
                    rm.href = Url.Content("~/home/NuevaFactura/" +model.Factura_EncabezadoId);
                }
            }
            else
            {
                rm.SetResponse(false, "El mínimo de atículos es: 1");
            }

            return Json(rm);
        }
    }
}
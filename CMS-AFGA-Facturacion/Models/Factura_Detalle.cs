namespace CMS_AFGA_Facturacion.Models
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Factura_Detalle
    {
        public int Factura_DetalleId { get; set; }

        public int Factura_EncabezadoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="La cantidad mínima de artículos es 1")]
        public int Factura_DetalleCantidad { get; set; }

        public int ArticuloId { get; set; }

        public virtual Articulo Articulo { get; set; }

        public virtual Factura_Encabezado Factura_Encabezado { get; set; }


        //Listar todos los articulos de factura que coincidan con el encabezado
        public List<Factura_Detalle> Listar(int Factura_EncabezadoId)
        {
            var detallesFacturas = new List<Factura_Detalle>();

            try
            {
                using (var ctx = new TestContext())
                {
                    //Se traen todos los detalles de facturas cuyo Factura_EncabezadoId coincida con el del encabezado consultado
                    detallesFacturas = ctx.Factura_Detalle.Include("Articulo")
                                                  .Include("Factura_Encabezado")
                                                  .Include("Factura_Encabezado.Cliente")
                                                  .Where(x => x.Factura_EncabezadoId == Factura_EncabezadoId)
                                                  .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return detallesFacturas;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new TestContext())
                {
                    ctx.Entry(this).State = EntityState.Added;

                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        }
    }
}

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

    public partial class Factura_Encabezado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura_Encabezado()
        {
            Factura_Detalle = new HashSet<Factura_Detalle>();
        }

        public int Factura_EncabezadoId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Factura_EncabezadoFecha { get; set; }

        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura_Detalle> Factura_Detalle { get; set; }


        //Listar todos los encabezados de factura pertenecientes a un cliente
        public List<Factura_Encabezado> Listar(int Cliente_id)
        {
            var encabezadosFacturas = new List<Factura_Encabezado>();

            try
            {
                using (var ctx = new TestContext())
                {
                    //Se traen todos los encabezados de facturas cuyo ClienteID coincida con el del cliente consultado
                    encabezadosFacturas = ctx.Factura_Encabezado.Include("Factura_Detalle")
                                                  .Include("Cliente")
                                                  .Include("Factura_Detalle.Articulo")
                                                  .Where(x => x.ClienteId == Cliente_id)
                                                  .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return encabezadosFacturas;
        }
        //Se obtiene el valor individual según el Id del cliente
        public Factura_Encabezado Obtener(int id)
        {
            var factura = new Factura_Encabezado();

            try
            {
                using (var ctx = new TestContext())
                {
                    //Se traen todos los encabezados de facturas cuyo ClienteID coincida con el del cliente consultado
                    factura = ctx.Factura_Encabezado.Include("Factura_Detalle")
                                       .Include("Cliente")
                                       .Include("Factura_Detalle.Articulo")
                                       .Where(x => x.Factura_EncabezadoId == id)
                                       .SingleOrDefault();
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return factura;
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

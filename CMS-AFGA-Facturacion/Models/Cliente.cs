namespace CMS_AFGA_Facturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Factura_Encabezado = new HashSet<Factura_Encabezado>();
        }

        public int ClienteId { get; set; }

        public int ClienteCedula { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteNombre { get; set; }

        [Required]
        [StringLength(100)]
        public string ClienteDireccion { get; set; }

        public int ClienteTelefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura_Encabezado> Factura_Encabezado { get; set; }

        //Listar a todos los clientes
        public List<Cliente> Listar()
        {
            var clientes = new List<Cliente>();

            try
            {
                using (var ctx = new TestContext())
                {
                    //Se traen todos los clientes con la máxima profundidad de datos
                    clientes = ctx.Cliente.Include("Factura_Encabezado")
                                            .Include("Factura_Encabezado.Factura_Detalle")
                                            .Include("Factura_Encabezado.Factura_Detalle.Articulo")
                                            .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return clientes;
        }

        public Cliente Obtener( int id)
        {
            var cliente = new Cliente();

            try
            {
                using (var ctx = new TestContext())
                {
                    cliente = ctx.Cliente.Where(x => x.ClienteId == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cliente;
        }
    }
}

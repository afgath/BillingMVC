namespace CMS_AFGA_Facturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Articulo")]
    public partial class Articulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articulo()
        {
            Factura_Detalle = new HashSet<Factura_Detalle>();
        }

        public int ArticuloId { get; set; }

        [Required]
        [StringLength(50)]
        public string ArticuloNombre { get; set; }

        [StringLength(1)]
        public string ArticuloDescripcion { get; set; }

        [Column(TypeName = "money")]
        public decimal ArticuloValor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura_Detalle> Factura_Detalle { get; set; }

        public List<Articulo> Todos(int Alumno_id = 0)
        {
            var articulos = new List<Articulo>();

            try
            {
                using (var ctx = new TestContext())
                {
                    //Se traen todos los artículos existentes en la base de datos
                        articulos = ctx.Articulo.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return articulos;
        }
    }
}

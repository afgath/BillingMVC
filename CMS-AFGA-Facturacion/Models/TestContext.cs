namespace CMS_AFGA_Facturacion.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Factura_Detalle> Factura_Detalle { get; set; }
        public virtual DbSet<Factura_Encabezado> Factura_Encabezado { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>()
                .Property(e => e.ArticuloNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Articulo>()
                .Property(e => e.ArticuloDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Articulo>()
                .Property(e => e.ArticuloValor)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Articulo>()
                .HasMany(e => e.Factura_Detalle)
                .WithRequired(e => e.Articulo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.ClienteNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.ClienteDireccion)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Factura_Encabezado)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Factura_Encabezado>()
                .HasMany(e => e.Factura_Detalle)
                .WithRequired(e => e.Factura_Encabezado)
                .WillCascadeOnDelete(false);
        }
    }
}

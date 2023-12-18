using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProviderWks.Domain.Entities
{
    public class TblProducto
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int IDProducto { get; set; }

        [Column("CODPRODUCTO")]
        public string CodProducto { get; set; }

        [Column("NOMBRE")]
        public string NombreProducto { get; set; }

        [Column("Valor")]
        public string Valor { get; set; }

        [Column("Cantidad")]
        public string Cantidad { get; set; }

        [Column("ESTADO")]
        public int Estado { get; set; }
    }
}

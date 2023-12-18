using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderWks.Domain.Entities
{
    public class TblUsers
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Estado { get; set; } = null!;
        [Required]
        public int Rol { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderWks.Domain.DTO
{
    public class Users
    { 
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int Rol { get; set; }
    }
}

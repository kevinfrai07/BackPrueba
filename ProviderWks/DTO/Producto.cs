using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProviderWks.Domain.DTO
{
    public class Producto
    {
        public int IDProducto { get; set; }

        public string CodProducto { get; set; }

        public string NombreProducto { get; set; }
        public string Valor { get; set; }

        public string Cantidad { get; set; }

        public int Estado { get; set; }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProviderWks.Domain.DTO;
using ProviderWks.Domain.Entities;
using ProviderWks.Persistence;
using System.Text.Json;

namespace ProviderWks.Service.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<ResponseDTO>
    {
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Producto";

                try
                {
                    var GetProducto = await _context.Producto.Where(u => u.IDProducto == request.IdProducto)
                        .FirstOrDefaultAsync();

                    if (GetProducto == null)
                    {
                        error = "Producto No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetProducto.CodProducto = request.Producto.CodProducto;
                    GetProducto.NombreProducto = request.Producto.NombreProducto;
                    GetProducto.Cantidad = request.Producto.Cantidad;
                    GetProducto.Estado = request.Producto.Estado;

                    var nroReg = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroReg > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = Convert.ToString(GetProducto.IDProducto)
                        };

                    }
                    else
                    {
                        error = "El Producto no fue Editado";
                        respuesta.responseStatus = 400;
                        respuesta.responseData = new
                        {
                            error = error
                        };
                    }

                    return respuesta;

                }
                catch (Exception e)
                {
                    var mensaje = $"ERROR No Controlado {e.Message} {e.InnerException}";
                    respuesta.responseStatus = 500;
                    respuesta.responseData = new
                    {
                        error = error
                    };


                    return respuesta;
                }
            }
        }
    }
}


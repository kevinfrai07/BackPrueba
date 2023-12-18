using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : IRequest<ResponseDTO>
    {
        public int IdProduct { get; set; }
        public Producto Producto { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Eliminando";

                try
                {
                    var GetProducto = await _context.Producto.Where(u => u.IDProducto == request.IdProduct)
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

                    GetProducto.Estado = 0;

                    //_context.Users.Remove(GetProducto); Caso tal eliminar la info
                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = Convert.ToString(GetProducto.IDProducto)
                        };

                    }
                    else
                    {
                        error = "El Producto no Eliminado";
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


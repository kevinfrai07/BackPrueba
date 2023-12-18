using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.ProductFeatures.Queries
{
    public class GetProductById : IRequest<ResponseDTO>
    {
        public int IDProducto { get; set; }

        public class GetProductByIdHandler : IRequestHandler<GetProductById, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetProductByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetProductById request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por ID";

                try
                {
                    var Producto = await _context.Producto.Where(x=> x.IDProducto == request.IDProducto)
                        .FirstOrDefaultAsync();
                    if (Producto == null)
                    {
                        error = "Producto no encontrado";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = "",
                        };

                        return respuesta;
                    }
                    respuesta.responseStatus = 200;
                    respuesta.responseData = Producto;
                    return respuesta;
                }
                catch (Exception e)
                {
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

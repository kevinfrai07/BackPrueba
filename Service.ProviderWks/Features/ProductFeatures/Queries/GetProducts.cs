using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Domain.Entities;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.ProductFeatures.Queries
{
    public class GetProducts : IRequest<ResponseDTO>
    {
        public class GetUsersHandler : IRequestHandler<GetProducts, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetProducts request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando";

                try
                {
                    var Producto = await _context.Producto.Select(x=> x)
                        .ToListAsync();
                    object responseData = new object();

                    if (Producto.Count == 0)
                    {
                        error = "Productos no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            IDProducto = "",
                        };

                        return respuesta;
                    }


                    respuesta.responseStatus = 200;
                    respuesta.responseData = responseData;
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

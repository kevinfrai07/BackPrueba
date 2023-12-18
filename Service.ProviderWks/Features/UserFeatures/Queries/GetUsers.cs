using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.UserFeatures.Queries
{
    public class GetUsers : IRequest<ResponseDTO>
    {
        public class GetUsersHandler : IRequestHandler<GetUsers, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetUsers request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var usuario = await _context.Users.Select(x=> x)
                        .ToListAsync();
                    object responseData = new object();

                    if (usuario.Count == 0)
                    {
                        error = "Usuarios no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = "",
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

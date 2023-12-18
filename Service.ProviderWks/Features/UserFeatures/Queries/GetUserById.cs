using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.UserFeatures.Queries
{
    public class GetUserById : IRequest<ResponseDTO>
    {
        public int IdUser { get; set; }

        public class GetUserByIdHandler : IRequestHandler<GetUserById, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUserByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetUserById request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var usuario = await _context.Users.Where(x=> x.IdUsuario == request.IdUser)
                        .FirstOrDefaultAsync();
                    if (usuario == null)
                    {
                        error = "Usuario no encontrado";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = "",
                        };

                        return respuesta;
                    }
                    respuesta.responseStatus = 200;
                    respuesta.responseData = new
                    {
                        userId = usuario.IdUsuario,
                        name = usuario.Nombre,
                        state = usuario.Estado,
                        rol = usuario.Rol,
                    };
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

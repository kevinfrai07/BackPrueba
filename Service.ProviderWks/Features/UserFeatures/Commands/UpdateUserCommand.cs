using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.UserFeatures.Commands
{
    public class UpdateUserCommand : IRequest<ResponseDTO>
    {
        public int IdUser { get; set; }
        public Users User { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public UpdateUserCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Usuario";

                try
                {
                    var GetUsuario = await _context.Users.Where(u => u.IdUsuario == request.IdUser)
                        .FirstOrDefaultAsync();

                    if (GetUsuario == null)
                    {
                        error = "Usuario No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetUsuario.Nombre = request.User.Nombre;
                    GetUsuario.Estado = request.User.Estado;
                    GetUsuario.Rol = request.User.Rol;

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = Convert.ToString(GetUsuario.IdUsuario)
                        };

                    }
                    else
                    {
                        error = "El usuario no Editado";
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


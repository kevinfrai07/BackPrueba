using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProviderWks.Domain.DTO;
using ProviderWks.Domain.Entities;
using ProviderWks.Persistence;
using System.Text.Json;

namespace ProviderWks.Service.Features.UserFeatures.Commands
{
    public class DeleteUserCommand : IRequest<ResponseDTO>
    {
        public int IdUser { get; set; }
        public Users User { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public DeleteUserCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Eliminando";

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

                    GetUsuario.Rol = 0;

                    //_context.Users.Remove(GetUsuario); Caso tal eliminar la info
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
                        error = "El usuario no Eliminado";
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


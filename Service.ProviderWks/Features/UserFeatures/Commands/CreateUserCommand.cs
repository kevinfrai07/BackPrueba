using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProviderWks.Domain.DTO;
using ProviderWks.Domain.Entities;
using ProviderWks.Persistence;
using System.Text.Json;

namespace ProviderWks.Service.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<ResponseDTO>
    {
        public Users User { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public CreateUserCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Usuario";

                try
                {
                    

                    //Verifica si ya existe el usuario registrado
                    var GetUsuario = await _context.Users.Where(u => u.Nombre == request.User.Nombre
                                                                        && u.Rol == request.User.Rol)
                        .FirstOrDefaultAsync();

                    if (GetUsuario != null)
                    {
                        error = "Ya existe un usuario con los datos suministrados.";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                   
                    var usuario = new TblUsers
                    {
                       Nombre = request.User.Nombre,
                       Estado = request.User.Estado,
                       Rol = request.User.Rol,
                    };

                    _context.Users.Add(usuario);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = Convert.ToString(usuario.IdUsuario)
                        };

                    }
                    else
                    {
                        error = "El usuario no fue creado";
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


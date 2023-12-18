using MediatR;
using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.DTO;
using ProviderWks.Domain.Entities;
using ProviderWks.Persistence;

namespace ProviderWks.Service.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<ResponseDTO>
    {
        public Producto Prod { get; set; }
        public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Producto";

                try
                {
                    

                    //Verifica si ya existe el Producto registrado
                    var GetProducto = await _context.Producto.Where(u => u.CodProducto == request.Prod.CodProducto)
                        .FirstOrDefaultAsync();

                    if (GetProducto != null)
                    {
                        error = "Ya existe un Producto con los datos suministrados.";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                   
                    var Producto = new TblProducto
                    {
                        CodProducto = request.Prod.CodProducto,
                        Cantidad = request.Prod.Cantidad,
                        Estado = request.Prod.Estado,
                        NombreProducto = request.Prod.NombreProducto
                    };

                    _context.Producto.Add(Producto);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = Convert.ToString(Producto.IDProducto)
                        };

                    }
                    else
                    {
                        error = "El Producto no fue creado";
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


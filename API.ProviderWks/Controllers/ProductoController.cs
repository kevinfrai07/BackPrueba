using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProviderWks.Domain.DTO;
using ProviderWks.Service.Features.ProductFeatures.Commands;
using ProviderWks.Service.Features.ProductFeatures.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProviderWks.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private const string get = "Product/v1/Products";
        private const string getById = "Product/v1/Products/{Id}";
        private const string Create = "Product/v1/Products";
        private const string Update = "Product/v1/Products/{Id}";
        private const string Delete = "Product/v1/Products/{Id}";

        #region Private fields

        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ProductController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Implementation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get All Products", Description = "Returns All Products", OperationId = "GetAllProducts")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new GetProducts { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get Product By Id", Description = "Return Product by Id", OperationId = "GetProductById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getById)]
        public async Task<ActionResult> GetProductById(string Id)
        {
            string cacheName = Id;
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

            if (_memoryCache.TryGetValue(cacheName, out ResponseDTO response))
            {
                return Ok(response);
            }

            response = await _mediator.Send(new GetProductById { IDProducto = int.Parse(Id), });
            _memoryCache.Set(cacheName, response, cacheOptions);
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create Producto", Description = "Create New Producto", OperationId = "CreateProduct")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreateProduct(Producto Product)
        {
            var response = await _mediator.Send(new CreateProductCommand { Prod = Product, });
            return StatusCode((int)response.responseStatus, response);
        }


        [SwaggerOperation(Summary = "Update Producto", Description = "Update Producto By Id", OperationId = "UpdateProduct")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPut(Update)]
        public async Task<ActionResult> UpdateProduct(string Id, [FromBody] Producto Product)
        {
            var response = await _mediator.Send(new UpdateProductCommand { IdProducto = int.Parse(Id), Producto = Product });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Delete Product", Description = "Delete Product By", OperationId = "DeleteProduct")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpDelete(Delete)]
        public async Task<ActionResult> DeleteProduct(string Id)
        {
            var response = await _mediator.Send(new DeleteProductCommand { IdProduct = int.Parse(Id)});
            return StatusCode((int)response.responseStatus, response);
        }
        #endregion
    }
}
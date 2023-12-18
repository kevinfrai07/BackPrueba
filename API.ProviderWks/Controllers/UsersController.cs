using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProviderWks.Domain.DTO;
using ProviderWks.Service.Features.UserFeatures.Commands;
using ProviderWks.Service.Features.UserFeatures.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProviderWks.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string get = "user/v1/users";
        private const string getById = "user/v1/users/{Id}";
        private const string Create = "user/v1/users";
        private const string Update = "user/v1/users/{Id}";
        private const string Delete = "user/v1/users/{Id}";

        #region Private fields

        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public UsersController(IMediator mediator, IMemoryCache memoryCache)
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
        [SwaggerOperation(Summary = "Get All Users", Description = "Returns All users", OperationId = "GetAllUsers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetUsers { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get User By Id", Description = "Return user by Id", OperationId = "GetUserById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getById)]
        public async Task<ActionResult> GetUserById(string Id)
        {
            string cacheName = Id;
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

            if (_memoryCache.TryGetValue(cacheName, out ResponseDTO response))
            {
                return Ok(response);
            }

            response = await _mediator.Send(new GetUserById { IdUser = int.Parse(Id), });
            _memoryCache.Set(cacheName, response, cacheOptions);
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create User", Description = "Create New User", OperationId = "CreateUser")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreateUser(Users User)
        {
            var response = await _mediator.Send(new CreateUserCommand { User = User, });
            return StatusCode((int)response.responseStatus, response);
        }


        [SwaggerOperation(Summary = "Update User", Description = "Update User By Id", OperationId = "UpdateUser")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPut(Update)]
        public async Task<ActionResult> UpdateUser(string Id, [FromBody] Users User)
        {
            var response = await _mediator.Send(new UpdateUserCommand { IdUser = int.Parse(Id), User = User });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "DeleteUser User", Description = "DeleteUser User By", OperationId = "DeleteUser")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpDelete(Delete)]
        public async Task<ActionResult> DeleteUser(string Id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { IdUser = int.Parse(Id)});
            return StatusCode((int)response.responseStatus, response);
        }
        #endregion
    }
}
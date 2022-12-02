using ELIXIRETD.DATA.CORE.ICONFIGURATION;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.EXTENSIONS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.HELPERS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELIXIRETD.API.Controllers.USER_CONTROLLER
{

    public class RoleController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("GetAllActiveRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _unitOfWork.Roles.GetAllActiveRoles();

            return Ok(roles);
        }


        [HttpPost]
        [Route("AddNewRole")]
        public async Task<IActionResult> AddNewRole(UserRole role)
        {

            if (await _unitOfWork.Roles.ValidateRoleExist(role.RoleName))
                return BadRequest("Role already exist, please try something else!");


            await _unitOfWork.Roles.AddNewRole(role);
            await _unitOfWork.CompleteAsync();

            return Ok(role);
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserRole role)
        {
            await _unitOfWork.Roles.UpdateRoleInfo(role);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully updated roles!");
        }


        [HttpPut]
        [Route("InActiveRoles")]
        public async Task<IActionResult> InActiveRoles([FromBody] UserRole role)
        {
            await _unitOfWork.Roles.InActiveRole(role);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully inactive role!");
        }


        [HttpPut]
        [Route("ActivateRoles")]
        public async Task<IActionResult> ActivateRoles([FromBody] UserRole role)
        {
            await _unitOfWork.Roles.ActivateRole(role);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully activate role!");
        }


        [HttpGet]
        [Route("GetAllRoleWithPagination/{status}")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoleWithPagination([FromRoute] bool status, [FromQuery] UserParams userParams)
        {
            var role = await _unitOfWork.Roles.GetAllRoleWithPagination(status, userParams);

            Response.AddPaginationHeader(role.CurrentPage, role.PageSize, role.TotalCount, role.TotalPages, role.HasNextPage, role.HasPreviousPage);

            var roleResult = new
            {
                role,
                role.CurrentPage,
                role.PageSize,
                role.TotalCount,
                role.TotalPages,
                role.HasNextPage,
                role.HasPreviousPage
            };

            return Ok(roleResult);
        }



        [HttpGet]
        [Route("GetAllRoleWithPaginationOrig/{status}")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoleWithPaginationOrig([FromRoute] bool status, [FromQuery] UserParams userParams, [FromQuery] string search)
        {

            if (search == null)

                return await GetAllRoleWithPagination(status, userParams);

            var role = await _unitOfWork.Roles.GetAllRoleWithPaginationOrig(userParams, status, search);


            Response.AddPaginationHeader(role.CurrentPage, role.PageSize, role.TotalCount, role.TotalPages, role.HasNextPage, role.HasPreviousPage);

            var roleResult = new
            {
                role,
                role.CurrentPage,
                role.PageSize,
                role.TotalCount,
                role.TotalPages,
                role.HasNextPage,
                role.HasPreviousPage
            };

            return Ok(roleResult);
        }

    }
}

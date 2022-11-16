using ELIXIRETD.DATA.CORE.ICONFIGURATION;
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


    }
}

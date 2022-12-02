using ELIXIRETD.DATA.CORE.ICONFIGURATION;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELIXIRETD.API.Controllers.USER_CONTROLLER
{
    public class ModuleController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }


        [HttpGet]
        [Route("GetAllActiveModules")]
        public async Task<IActionResult> GetAllActiveModules()
        {
            var roles = await _unitOfWork.Modules.GetAllActiveModules();

            return Ok(roles);
        }


        [HttpGet]
        [Route("GetAllActiveModules")]
        public async Task<IActionResult> GetAllIncActiveModules()
        {
            var roles = await _unitOfWork.Modules.GetAllInActiveModules();

            return Ok(roles);
        }






    }
}

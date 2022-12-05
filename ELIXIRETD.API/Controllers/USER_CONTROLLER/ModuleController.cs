using ELIXIRETD.DATA.CORE.ICONFIGURATION;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.EXTENSIONS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.HELPERS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
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


        [HttpPost]
        [Route("AddNewModule")]
        public async Task<IActionResult> CreateModule(Module module)
        {
        
                var getMainMenuId = await _unitOfWork.Modules.CheckMainMenu(module.MainMenuId);

                if (getMainMenuId == false)
                    return BadRequest("MainMenu doesn't exist, Please input data first!");

                if (await _unitOfWork.Modules.SubMenuNameExist(module.SubMenuName))
                    return BadRequest("SubMenu Already Exist!, Please try something else!");

                if (await _unitOfWork.Modules.ModuleNameExist(module.ModuleName))
                    return BadRequest("ModuleName Already Exist!, Please try something else!");

                await _unitOfWork.Modules.AddNewModule(module);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetModules", new { module.Id }, module);
        }

        [HttpPut]
        [Route("UpdateModule/{id}")]
        public async Task<IActionResult> UpdateModuleById(int id, [FromBody] Module module)
        {
            if (id != module.Id)
                return BadRequest();

            await _unitOfWork.Modules.UpdateModule(module);
            await _unitOfWork.CompleteAsync();

            return Ok(module);
        }

        [HttpPut]
        [Route("InActiveModule")]
        public async Task<IActionResult> InActiveModule([FromBody] Module module)
        {

            await _unitOfWork.Modules.InActiveModule(module);
            await _unitOfWork.CompleteAsync();

            return new JsonResult("Successfully InActive Module!");
        }

        [HttpPut]
        [Route("ActivateModule")]
        public async Task<IActionResult> ActivateModule([FromBody] Module module)
        {
    
            await _unitOfWork.Modules.ActivateModule(module);
            await _unitOfWork.CompleteAsync();

            return new JsonResult("Successfully Activate Module!");
        }


        [HttpGet]
        [Route("GetAllModulesWithPagination/{status}")]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAllModulesWithPagination([FromRoute] bool status, [FromQuery] UserParams userParams)
        {
            var module = await _unitOfWork.Modules.GetAllModulessWithPagination(status, userParams);

            Response.AddPaginationHeader(module.CurrentPage, module.PageSize, module.TotalCount, module.TotalPages, module.HasNextPage, module.HasPreviousPage);

            var moduleResult = new
            {
                module,
                module.CurrentPage,
                module.PageSize,
                module.TotalCount,
                module.TotalPages,
                module.HasNextPage,
                module.HasPreviousPage
            };

            return Ok(moduleResult);
        }

        [HttpGet]
        [Route("GetAllModulesWithPaginationOrig/{status}")]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAllUsersWithPaginationOrig([FromRoute] bool status, [FromQuery] UserParams userParams, [FromQuery] string search)
        {

            if (search == null)

                return await GetAllModulesWithPagination(status, userParams);

            var module = await _unitOfWork.Modules.GetModulesByStatusWithPaginationOrig(userParams, status, search);


            Response.AddPaginationHeader(module.CurrentPage, module.PageSize, module.TotalCount, module.TotalPages, module.HasNextPage, module.HasPreviousPage);

            var moduleResult = new
            {
                module,
                module.CurrentPage,
                module.PageSize,
                module.TotalCount,
                module.TotalPages,
                module.HasNextPage,
                module.HasPreviousPage
            };

            return Ok(moduleResult);
        }


    }
}

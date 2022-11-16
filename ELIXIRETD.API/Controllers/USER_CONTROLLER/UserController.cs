using ELIXIRETD.DATA.CORE.ICONFIGURATION;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELIXIRETD.API.Controllers.USER_CONTROLLER
{
   
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;

        public UserController(StoreContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _unitOfWork.Users.GetAllActiveUsers();

            return Ok(user);
        }

        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> AddNewUser(User user)
        {

            await _unitOfWork.Users.AddNewUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody]User user)
        {
            await _unitOfWork.Users.UpdateUserInfo(user);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully updated!");
        }

        [HttpPut]
        [Route("InactiveUser")]
        public async Task<IActionResult> InActiveUser([FromBody]User user)
        {
            await _unitOfWork.Users.InActiveUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully inactive user!");
        }


        [HttpPut]
        [Route("ActivateUser")]
        public async Task<IActionResult> ActivateUser([FromBody] User user)
        {
            await _unitOfWork.Users.InActiveUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully activate user!");
        }



        //------------DEPARTMENT

        [HttpGet]
        [Route("GetAllActiveDepartment")]
        public async Task<IActionResult> GetAllActiveDepartment()
        {
            var dep = await _unitOfWork.Users.GetAllActiveDepartment();

            return Ok(dep);
        }

        [HttpPost]
        [Route("AddNewDepartment")]
        public async Task<IActionResult> AddNewDepartment(Department department)
        {

            await _unitOfWork.Users.AddNewDepartment(department);
            await _unitOfWork.CompleteAsync();

            return Ok(department);
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
        {
            await _unitOfWork.Users.UpdateDepartment(department);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully updated!");
        }


        [HttpPut]
        [Route("InActiveDepartment")]
        public async Task<IActionResult> InActiveDepartment([FromBody] Department department)
        {
            await _unitOfWork.Users.InActiveDepartment(department);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully inactive department!");
        }


        [HttpPut]
        [Route("ActivateDepartment")]
        public async Task<IActionResult> ActivateDepartment([FromBody] Department department)
        {
            await _unitOfWork.Users.InActiveDepartment(department);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully activate department!");
        }


    }
}

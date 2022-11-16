using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<UserDto>> GetAllActiveUsers();
        Task<IReadOnlyList<UserDto>> GetAllInActiveUsers();
        Task<bool> AddNewUser(User user);
        Task<bool> UpdateUserInfo(User user);
        Task<bool> InActiveUser(User user);
        Task<bool> ActivateUser(User user);

    }
}

using ELIXIRETD.DATA.JWT.AUTHENTICATION;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.ICONFIGURATION
{
    public interface IUserService
    {

        AuthenticateResponse Authenticate(AuthenticateRequest request);

    }
}

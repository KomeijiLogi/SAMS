using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Users.Dto
{
    public class GetUserForEditOutput
    {
        public UserEditDto User { get; set; }
        public UserRoleDto[] Roles { get; set; }
    }
}

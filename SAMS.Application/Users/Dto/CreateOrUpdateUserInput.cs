using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Users.Dto
{
    public class CreateOrUpdateUserInput
    {
        public UserEditDto User { get; set; }

        public string [] AssignedRoleNames { get; set; }
    }
}

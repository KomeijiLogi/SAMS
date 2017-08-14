using Abp.Domain.Entities;

namespace SAMS.Users.Dto
{
    public class UserEditDto:IPassivable
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string EmailAddress { get; set; }
        public string Surname { get; set; }
        public bool ShouldChangePasswordOnNextLogin { get; set; }
    }
}
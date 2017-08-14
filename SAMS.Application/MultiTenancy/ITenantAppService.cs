using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SAMS.MultiTenancy.Dto;

namespace SAMS.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}

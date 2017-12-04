using Abp.Application.Services.Dto;
using SAMS.Accessories.Dtos;

namespace SAMS.Accessories
{
    public interface IAccessoryAppService
    {
        PagedResultDto<GetAccessoryDto> GetAccessory(GetAccessoryInput input);
        void Delete(string id);
        void Create(CreateInput input);
        void Edit(EditInput input);
        GetDetailOutput GetDetail(string id);
    }
}
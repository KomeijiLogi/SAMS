using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.Faults.Dtos;


namespace SAMS.Faults
{
    public interface IFaultAppService
    {
        ListResultDto<GetFaultDetailOutput>GetAllDetails();
        void Create(CreateInput input);
        void Edit(EditInput input);
        void Delete(int id);
        ListResultDto<FaultListDto> GetFaults();
        GetFaultDetailOutput GetDetail(int id);
    }
}
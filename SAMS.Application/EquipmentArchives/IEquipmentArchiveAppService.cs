using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SAMS.EquipmentArchives.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.EquipmentArchives
{
    public interface IEquipmentArchiveAppService : IApplicationService
    {
        PagedResultDto<EquipmentArchiveListDto> GetEquipmentArchives(GetEquipmentArchivesInput input);
        EquipmentArchiveDetailDto GetDetail(int Id);
        void Create(CreateEquipmentArchiveInput input);
        void Edit(EditEquipmentArchiveInput input);
        void Delete(int id);
        bool CheckSerialNo(int? id,string serialNo);
        EquipmentArchiveDetailDto GetArchiveBySerialNo(string serialNo);
    }
}

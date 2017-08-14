using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderPhoto))]
    public class FaultPhotoDto:EntityDto
    {
        public virtual string FilePath { get; set; }
        public virtual int ParentId { get; set; }
    }
}
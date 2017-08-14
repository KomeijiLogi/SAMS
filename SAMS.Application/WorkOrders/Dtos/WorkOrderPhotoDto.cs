using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderPhoto))]
    public class WorkOrderPhotoDto : EntityDto
    {
        public virtual int BillId { get; set; }
      
        public virtual string FilePath { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FileType { get; set; }

    }
}

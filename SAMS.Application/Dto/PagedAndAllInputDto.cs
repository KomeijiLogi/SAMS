using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Dto
{
    public class PagedAndAllInputDto:PagedAllInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndAllInputDto()
        {
            MaxResultCount = 20;
        }
    }
}

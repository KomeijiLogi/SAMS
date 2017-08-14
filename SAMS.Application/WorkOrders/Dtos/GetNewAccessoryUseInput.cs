using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using SAMS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class GetNewAccessoryUseInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if(string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime desc";
            }
        }
        public GetNewAccessoryUseWhere Where { get; set; }

    }
}

using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Dto
{
   public  class PagedAllInputDto:IInputDto,IPagedResultRequest
    {

        
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public PagedAllInputDto()
        {
            MaxResultCount = 10000;
        }



        //public string Where { get; set; }
    }
}

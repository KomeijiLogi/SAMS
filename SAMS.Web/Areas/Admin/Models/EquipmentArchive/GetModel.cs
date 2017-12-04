using Abp.Application.Services.Dto;
using SAMS.EquipmentArchives.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.EquipmentArchive
{
    public class GetModel
    {
        public PagedResultDto<EquipmentArchiveListDto> EquipmentArchives { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Start
        {
            get
            {
                return (PageIndex - 1) * PageSize + 1;
            }
        }

        public int End
        {
            get
            {
                int end = Start + PageSize - 1;
                if (end > EquipmentArchives.TotalCount) end = EquipmentArchives.TotalCount;
                return end;
            }
        }
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling((double)EquipmentArchives.TotalCount / (double)PageSize);
            }
        }
        public List<int> PageNumbers
        {
            get
            {
                int step = 5;//偏移量
                int leftNum = 0;//左边界
                int rightNum = 0;//右边界


                if (PageIndex - step < 1)
                {
                    leftNum = 1;
                }
                else
                {
                    leftNum = PageIndex - step;
                }
                if (PageIndex + step > PageCount)
                {
                    rightNum = PageCount;
                }
                else
                {
                    rightNum = PageIndex + step;
                }

                var list = new List<int>();

                for (int i = leftNum; i <= rightNum; i++)
                {
                    list.Add(i);
                }

                return list;

            }
        }
    }
}
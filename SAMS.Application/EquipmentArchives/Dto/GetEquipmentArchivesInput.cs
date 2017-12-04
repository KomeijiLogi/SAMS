using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using SAMS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.EquipmentArchives.Dto
{
    public class GetEquipmentArchivesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SerialNo { get; set; }
        public string CustomerName { get; set; }
        public string ProductId { get; set; }
        public DateTime? StartInstallDate { get; set; }
        public DateTime? EndInstallDate { get; set; }
        public DateTime? StartServiceDate { get; set; }
        public DateTime? EndServiceDate { get; set; }
        public DateTime? StartWarrenty { get; set; }
        public DateTime? EndWarrenty { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "SerialNo desc";
            }
        }

    }
}

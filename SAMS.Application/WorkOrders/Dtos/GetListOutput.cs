using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class GetListOutput
    {
        public List<GetWorkOrderListDto> WorkOrders { get; set; }
    }
}

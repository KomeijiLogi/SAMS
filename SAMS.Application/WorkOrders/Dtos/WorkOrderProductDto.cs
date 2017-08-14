using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class WorkOrderProductDto
    {
        public virtual string Name { get; set; }
        public virtual string Id { get; set; }
        public virtual string Model { get; set; }
    }
}
